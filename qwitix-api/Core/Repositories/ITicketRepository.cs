using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);

        Task<IEnumerable<Ticket>> GetAll(string eventId, int offset, int limit);

        Task<Ticket?> GetById(string id);

        Task UpdateById(string id, Ticket ticket);

        Task DeleteById(string id);
    }
}
