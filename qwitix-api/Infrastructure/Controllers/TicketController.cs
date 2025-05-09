using System.ComponentModel.DataAnnotations;
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

        [HttpPost("ticket", Name = "CreateTicket")]
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

        [HttpPost("ticket/buy", Name = "BuyTicket")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBuyTicketDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuyById(BuyTicketDTO buyTicketDTO)
        {
            var userId = User.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID not found in token.");

            var response = await _ticketService.BuyById(userId, buyTicketDTO);

            return Ok(response);
        }

        [HttpGet("ticket/list", Name = "GetTicketList")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseTicketWithSoldDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([Required] string eventId)
        {
            IEnumerable<ResponseTicketWithSoldDTO> tickets = await _ticketService.GetAll(eventId);

            return Ok(tickets);
        }

        [HttpGet("ticket/{id}", Name = "GetTicket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTicketWithSoldDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseTicketWithSoldDTO ticket = await _ticketService.GetById(id);

            return Ok(ticket);
        }

        [HttpPatch("ticket/{id}", Name = "UpdateTicket")]
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

        [HttpDelete("ticket/{id}", Name = "DeleteTicket")]
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
