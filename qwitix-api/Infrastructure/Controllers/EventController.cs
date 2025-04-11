using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.EventService;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("event")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateEventDTO eventDTO)
        {
            await _eventService.Create(eventDTO);

            return Created();
        }

        [HttpPost("event/{id}/publish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Publish(string id, PublishEventDTO publishEventDTO)
        {
            await _eventService.Publish(id, publishEventDTO);

            return Ok();
        }

        [HttpGet("events")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseEventDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            [Required] string organizerId,
            int offset,
            int limit
        )
        {
            IEnumerable<ResponseEventDTO> events = await _eventService.GetAll(
                organizerId,
                offset,
                limit
            );

            return Ok(events);
        }

        [HttpGet("event/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseEventDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseEventDTO eventDto = await _eventService.GetById(id);

            return Ok(eventDto);
        }

        [HttpPatch("event/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, UpdateEventDTO eventDTO)
        {
            await _eventService.UpdateById(id, eventDTO);

            return Ok();
        }

        [HttpDelete("event/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _eventService.DeleteById(id);

            return NoContent();
        }
    }
}
