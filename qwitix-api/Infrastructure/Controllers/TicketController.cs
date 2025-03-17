using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.TicketService;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("/ticket")]
        public async Task Create(CreateTicketDTO ticketDTO)
        {
            await _ticketService.Create(ticketDTO);
        }

        [HttpPost("/ticket/buy/{id}")]
        public async Task BuyById(string id)
        {
            await _ticketService.BuyById(id);
        }

        [HttpPost("/ticket/refund/{id}")]
        public async Task RefundById(string id)
        {
            await _ticketService.RefundById(id);
        }

        [HttpGet("/tickets")]
        public async Task<IEnumerable<ResponseTicketDTO>> GetAll(
            string eventId,
            int offset,
            int limit
        )
        {
            return await _ticketService.GetAll(eventId, offset, limit);
        }

        [HttpGet("/ticket/{id}")]
        public async Task<ResponseTicketDTO> GetById(string id)
        {
            return await _ticketService.GetById(id);
        }

        [HttpPatch("/ticket/{id}")]
        public async Task UpdateById(string id, UpdateTicketDTO ticket)
        {
            await _ticketService.GetById(id);
        }

        [HttpDelete("/ticket/{id}")]
        public async Task DeleteById(string id)
        {
            await _ticketService.DeleteById(id);
        }
    }
}
