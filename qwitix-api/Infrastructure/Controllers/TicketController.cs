using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.TicketService;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("ticket")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateTicketDTO ticketDTO)
        {
            await _ticketService.Create(ticketDTO);

            return Created();
        }

        [HttpPost("ticket/buy")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuyById(BuyTicketDTO buyTicketDTO)
        {
            var userId = User.FindFirst("user_id")?.Value;

            var response = await _ticketService.BuyById(userId, buyTicketDTO);

            return Ok(response);
        }

        [HttpGet("tickets")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseTicketDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string eventId)
        {
            IEnumerable<ResponseTicketDTO> tickets = await _ticketService.GetAll(eventId);

            return Ok(tickets);
        }

        [HttpGet("ticket/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTicketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseTicketDTO ticket = await _ticketService.GetById(id);

            return Ok(ticket);
        }

        [HttpPatch("ticket/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateById(string id, UpdateTicketDTO ticket)
        {
            await _ticketService.UpdateById(id, ticket);

            return Ok();
        }

        [HttpDelete("ticket/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _ticketService.DeleteById(id);

            return NoContent();
        }
    }
}
