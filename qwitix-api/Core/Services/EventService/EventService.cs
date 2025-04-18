using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.EventService.DTOs;
using qwitix_api.Infrastructure.Integration.StripeIntegration;

namespace qwitix_api.Core.Services.EventService
{
    public class EventService(
        IEventRepository eventRepository,
        ITicketRepository ticketRepository,
        IOrganizerRepository organizerRepository,
        StripeIntegration stripeIntegration,
        IMapper<CreateEventDTO, Event> createEventMapper,
        IMapper<ResponseEventDTO, Event> responseEventMapper
    )
    {
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly ITicketRepository _ticketRepository = ticketRepository;
        private readonly IOrganizerRepository _organizerRepository = organizerRepository;
        private readonly StripeIntegration _stripeIntegration = stripeIntegration;
        private readonly IMapper<CreateEventDTO, Event> _createEventMapper = createEventMapper;
        private readonly IMapper<ResponseEventDTO, Event> _responseEventMapper =
            responseEventMapper;

        public async Task Create(CreateEventDTO eventDTO)
        {
            var organizer =
                await _organizerRepository.GetById(eventDTO.OrganizerId)
                ?? throw new NotFoundException($"Organizer not found.");

            var eventModel = _createEventMapper.ToEntity(eventDTO);

            await _eventRepository.Create(eventModel);
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

        public async Task<IEnumerable<ResponseEventDTO>> GetAll(
            string organizerId,
            int offset,
            int limit
        )
        {
            var events = await _eventRepository.GetAll(organizerId, offset, limit);

            return _responseEventMapper.ToDtoList(events);
        }

        public async Task<ResponseEventDTO> GetById(string id)
        {
            var eventModel =
                await _eventRepository.GetById(id)
                ?? throw new NotFoundException($"Event not found.");

            return _responseEventMapper.ToDto(eventModel);
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
    }
}
