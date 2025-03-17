using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Models;
using qwitix_api.Core.Services.EventService;
using qwitix_api.Core.Services.EventService.DTOs;
using qwitix_api.Core.Services.OrganizerService;
using qwitix_api.Core.Services.OrganizerService.DTOs;
using qwitix_api.Core.Services.TicketService;

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

        [HttpPost("event/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateEventDTO eventDTO)
        {
            try
            {
                await _eventService.Create(eventDTO);

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("events")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseEventDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string organizerId, int offset, int limit)
        {
            try
            {
                IEnumerable<ResponseEventDTO> events = await _eventService.GetAll(
                    organizerId,
                    offset,
                    limit
                );

                return Ok(events);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("event/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseEventDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                ResponseEventDTO eventDto = await _eventService.GetById(id);

                return Ok(eventDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("event/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, UpdateEventDTO eventDTO)
        {
            try
            {
                await _eventService.UpdateById(id, eventDTO);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("event/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(string id)
        {
            try
            {
                await _eventService.DeleteById(id);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
