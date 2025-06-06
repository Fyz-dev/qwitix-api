﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Processors;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.EventService.DTOs;
using qwitix_api.Core.Services.TicketService.DTOs;
using qwitix_api.Infrastructure.Integration.StripeIntegration;

namespace qwitix_api.Core.Services.EventService
{
    public class EventService(
        IEventRepository eventRepository,
        ITicketRepository ticketRepository,
        IOrganizerRepository organizerRepository,
        IBlobStorageRepository blobStorageRepository,
        IUrlProcessor urlProcessor,
        StripeIntegration stripeIntegration,
        IMapper<CreateEventDTO, Event> createEventMapper,
        IMapper<ResponseEventDTO, Event> responseEventMapper,
        IMapper<ResponseTicketDTO, Ticket> responseTicketMapper
    )
    {
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly ITicketRepository _ticketRepository = ticketRepository;
        private readonly IOrganizerRepository _organizerRepository = organizerRepository;
        private readonly IBlobStorageRepository _blobStorageRepository = blobStorageRepository;
        private readonly IUrlProcessor _urlProcessor = urlProcessor;
        private readonly StripeIntegration _stripeIntegration = stripeIntegration;
        private readonly IMapper<CreateEventDTO, Event> _createEventMapper = createEventMapper;
        private readonly IMapper<ResponseEventDTO, Event> _responseEventMapper =
            responseEventMapper;
        private readonly IMapper<ResponseTicketDTO, Ticket> _responseTicketMapper =
            responseTicketMapper;

        public async Task<ResponseEventDTO> Create(CreateEventDTO eventDTO)
        {
            var organizer =
                await _organizerRepository.GetById(eventDTO.OrganizerId)
                ?? throw new NotFoundException($"Organizer not found.");

            var eventModel = _createEventMapper.ToEntity(eventDTO);

            await _eventRepository.Create(eventModel);

            return _responseEventMapper.ToDto(eventModel);
        }

        public async Task Publish(string id, PublishEventDTO publishEventDTO)
        {
            var eventModel =
                await _eventRepository.GetById(id)
                ?? throw new NotFoundException($"Event not found.");

            if (eventModel.Status != EventStatus.Draft)
                throw new ValidationException(
                    "The event is already been published, cancelled or rescheduled."
                );

            if (publishEventDTO.StartDate < DateTime.UtcNow)
                throw new ValidationException("StartDate cannot be earlier than the current date.");

            eventModel.Status = EventStatus.Scheduled;
            eventModel.StartDate = publishEventDTO.StartDate;
            eventModel.EndDate = publishEventDTO.EndDate;

            var tickets = await _ticketRepository.GetAll(id);

            foreach (var ticket in tickets)
            {
                var price = await _stripeIntegration.CreatePriceAsync(
                    ticket.Price,
                    "usd",
                    ticket.Id
                );

                ticket.StripePriceId = price.Id;
            }

            await _eventRepository.UpdateById(id, eventModel);
            await _ticketRepository.UpdateById(tickets);
        }

        public async Task<PaginationResponse<ResponseEventDTO>> GetAll(
            string? organizerId,
            int offset,
            int limit,
            List<EventStatus>? statuses = null,
            string? searchQuery = null,
            List<string>? categories = null
        )
        {
            var (events, totalCount) = await _eventRepository.GetAll(
                organizerId,
                offset,
                limit,
                statuses,
                searchQuery,
                categories
            );

            bool hasNextPage = (limit > 0) && (offset + limit < totalCount);

            var eventDTOs = events
                .Select(eventModel =>
                {
                    var eventDto = _responseEventMapper.ToDto(eventModel);

                    if (!string.IsNullOrEmpty(eventModel.ImgBlobName))
                        eventDto.ImgUrl = _urlProcessor.GetMediaUrl(eventModel.ImgBlobName);

                    return eventDto;
                })
                .ToList();

            foreach (var eventDTO in eventDTOs)
            {
                var tickets = await _ticketRepository.GetAll(eventDTO.Id);
                eventDTO.Tickets = tickets.Select(ticket => _responseTicketMapper.ToDto(ticket));
            }

            return new PaginationResponse<ResponseEventDTO>
            {
                Items = eventDTOs,
                TotalCount = totalCount,
                HasNextPage = hasNextPage,
            };
        }

        public async Task<ResponseEventDTO> GetById(string id)
        {
            var eventModel =
                await _eventRepository.GetById(id)
                ?? throw new NotFoundException($"Event not found.");

            var dto = _responseEventMapper.ToDto(eventModel);

            if (!string.IsNullOrEmpty(eventModel.ImgBlobName))
                dto.ImgUrl = _urlProcessor.GetMediaUrl(eventModel.ImgBlobName);

            var tickets = await _ticketRepository.GetAll(id);
            dto.Tickets = tickets.Select(ticket => _responseTicketMapper.ToDto(ticket));

            return dto;
        }

        public async Task UpdateById(string id, UpdateEventDTO eventDTO)
        {
            var eventModel =
                await _eventRepository.GetById(id)
                ?? throw new NotFoundException("Event not found.");

            if (eventModel.Status != EventStatus.Draft)
                throw new ValidationException(
                    "It is not possible to update an event that has already been published, cancelled or rescheduled."
                );

            PatchHelper.ApplyPatch(eventDTO, eventModel, nameof(eventDTO.Venue));

            if (eventDTO.Venue is not null)
                PatchHelper.ApplyPatch(eventDTO.Venue, eventModel.Venue);

            await _eventRepository.UpdateById(id, eventModel);
        }

        public async Task DeleteById(string id)
        {
            var eventModel =
                await _eventRepository.GetById(id)
                ?? throw new NotFoundException($"Event not found.");

            if (eventModel.Status != EventStatus.Draft)
                throw new ValidationException(
                    "It is not possible to delete an event that has already been published, cancelled or rescheduled."
                );

            await _eventRepository.DeleteById(id);
        }

        public async Task<IEnumerable<string>> GetUniqueCategories()
        {
            var categories = await _eventRepository.GetUniqueCategories();

            return categories;
        }

        public async Task<string> UploadImage(string eventId, UploadImageDTO imageDTO)
        {
            var eventModel =
                await _eventRepository.GetById(eventId)
                ?? throw new NotFoundException("Event not found.");

            if (eventModel.Status != EventStatus.Draft)
                throw new ValidationException("You can only upload an image for a draft event.");

            var blobName = await _blobStorageRepository.UploadFileAsync(imageDTO.Image, eventId);
            eventModel.ImgBlobName = blobName;

            await _eventRepository.UpdateById(eventId, eventModel);

            return _urlProcessor.GetMediaUrl(blobName);
        }

        public async Task DeleteImage(string eventId)
        {
            var eventModel =
                await _eventRepository.GetById(eventId)
                ?? throw new NotFoundException("Event not found.");

            if (eventModel.Status != EventStatus.Draft)
                throw new ValidationException("You can only delete an image from a draft event.");

            if (!string.IsNullOrEmpty(eventModel.ImgBlobName))
            {
                await _blobStorageRepository.DeleteFileAsync(eventModel.ImgBlobName);

                eventModel.ImgBlobName = null;

                await _eventRepository.UpdateById(eventId, eventModel);
            }
        }
    }
}
