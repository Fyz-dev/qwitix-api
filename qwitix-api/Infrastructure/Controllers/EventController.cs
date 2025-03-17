using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.EventService;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("/event/{id}")]
        public async Task Create(CreateEventDTO eventDTO)
        {
            await _eventService.Create(eventDTO);
        }

        [HttpGet("/events")]
        public async Task<IEnumerable<ResponseEventDTO>> GetAll(
            string organizerId,
            int offset,
            int limit
        )
        {
            return await _eventService.GetAll(organizerId, offset, limit);
        }

        [HttpGet("/event/{id}")]
        public async Task<ResponseEventDTO> GetById(string id)
        {
            return await _eventService.GetById(id);
        }

        [HttpPatch("/event/{id}")]
        public async Task UpdateById(string id, UpdateEventDTO eventDTO)
        {
            await _eventService.UpdateById(id, eventDTO);
        }

        [HttpDelete("/event/{id}")]
        public async Task DeleteById(string id)
        {
            await _eventService.DeleteById(id);
        }
    }
}
