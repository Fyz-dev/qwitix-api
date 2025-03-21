using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using qwitix_api.Core.Services.TicketService;
using qwitix_api.Core.Services.TicketService.DTOs;
using qwitix_api.Core.Services.TransactionService.DTOs;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateTicketDTO ticketDTO)
        {
            await _ticketService.Create(ticketDTO);

            return Created();
        }

        [HttpPost("ticket/buy/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuyById(string id)
        {
            await _ticketService.BuyById(id);

            return Ok();
        }

        [HttpPost("ticket/refund/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefundById(string id)
        {
            await _ticketService.RefundById(id);

            return Ok();
        }

        [HttpGet("tickets")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseTicketDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string eventId, int offset, int limit)
        {
            IEnumerable<ResponseTicketDTO> tickets = await _ticketService.GetAll(
                eventId,
                offset,
                limit
            );

            return Ok(tickets);
        }

        [HttpGet("ticket/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTicketDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseTicketDTO ticket = await _ticketService.GetById(id);

            return Ok(ticket);
        }

        [HttpPatch("ticket/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, UpdateTicketDTO ticket)
        {
            await _ticketService.GetById(id);

            return Ok();
        }

        [HttpDelete("ticket/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _ticketService.DeleteById(id);

            return NoContent();
        }
    }
}
