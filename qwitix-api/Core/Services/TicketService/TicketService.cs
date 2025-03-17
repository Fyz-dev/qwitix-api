using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Core.Services.TicketService
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Create(CreateTicketDTO ticketDTO)
        {
            throw new NotImplementedException();
        }

        public async Task BuyById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task RefundById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ResponseTicketDTO>> GetAll(
            string eventId,
            int offset,
            int limit
        )
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseTicketDTO> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateById(string id, UpdateTicketDTO ticket)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
