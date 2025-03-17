using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api.Core.Services.EventService
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Create(CreateEventDTO eventDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ResponseEventDTO>> GetAll(
            string organizerId,
            int offset,
            int limit
        )
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseEventDTO> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateById(string id, UpdateEventDTO eventDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
