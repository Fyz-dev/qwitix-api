using qwitix_api.Core.Entities;

namespace qwitix_api.Core.Repositories
{
    public interface ITicketRepository
    {
        Task Create(ITicket eventDTO);

        Task BuyById(string id);

        Task RefundById(string id);

        Task<IEnumerable<ITicket>> GetAll(string eventId, int offset, int limit);

        Task<ITicket> GetById(string id);

        Task UpdateById(string id, ITicket eventDTO);

        Task DeleteById(string id);
    }
}
