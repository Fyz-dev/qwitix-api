using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Helpers;
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

        [HttpPost("event", Name = "CreateEvent"), DisableRequestSizeLimit]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CreateEventDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateEventDTO eventDTO)
        {
            await _eventService.Create(eventDTO);

            return Created();
        }

        [HttpPost("event/{id}/publish", Name = "PublishEvent")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Publish(string id, PublishEventDTO publishEventDTO)
        {
            await _eventService.Publish(id, publishEventDTO);

            return Ok();
        }

        [HttpGet("event/list", Name = "GetEventList")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(PaginationResponse<ResponseEventDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            string? organizerId,
            int offset,
            int limit,
            EventStatus? status = null,
            string? searchQuery = null,
            [FromQuery(Name = "categories")] List<string>? categories = null
        )
        {
            PaginationResponse<ResponseEventDTO> events = await _eventService.GetAll(
                organizerId,
                offset,
                limit,
                status,
                searchQuery,
                categories
            );

            return Ok(events);
        }

        [HttpGet("event/{id}", Name = "GetEvent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseEventDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseEventDTO eventDto = await _eventService.GetById(id);

            return Ok(eventDto);
        }

        [HttpPatch("event/{id}", Name = "UpdateEvent"), DisableRequestSizeLimit]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateEventDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, [FromBody] UpdateEventDTO eventDTO)
        {
            await _eventService.UpdateById(id, eventDTO);

            return Ok();
        }

        [HttpDelete("event/{id}", Name = "DeleteEvent")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _eventService.DeleteById(id);

            return NoContent();
        }

        [HttpGet("event/categories", Name = "GetEventCategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUniqueCategories()
        {
            IEnumerable<string> categories = await _eventService.GetUniqueCategories();

            return Ok(categories);
        }
    }
}
