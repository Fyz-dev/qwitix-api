using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api.Core.Services.EventService
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper<CreateEventDTO, Event> _createEventMapper;
        private readonly IMapper<ResponseEventDTO, Event> _responseEventMapper;

        public EventService(
            IEventRepository eventRepository,
            IMapper<CreateEventDTO, Event> createEventMapper,
            IMapper<ResponseEventDTO, Event> responseEventMapper
        )
        {
            _eventRepository = eventRepository;
            _createEventMapper = createEventMapper;
            _responseEventMapper = responseEventMapper;
        }

        public async Task Create(CreateEventDTO eventDTO)
        {
            var eventModel = _createEventMapper.ToEntity(eventDTO);

            await _eventRepository.Create(eventModel);
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

            PatchHelper.ApplyPatch(eventDTO, eventModel, nameof(eventDTO.Venue));

            if (eventDTO.Venue is not null)
                PatchHelper.ApplyPatch(eventDTO.Venue, eventModel.Venue);

            await _eventRepository.UpdateById(id, eventModel);
        }

        public async Task DeleteById(string id)
        {
            await _eventRepository.DeleteById(id);
        }
    }
}
