using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> Create(Ticket ticket);

        Task<IEnumerable<Ticket>> GetAll(string eventId);

        Task<Ticket?> GetById(string id);

        Task<IEnumerable<Ticket>> GetById(params string[] ids);

        Task UpdateById(string id, Ticket ticket);

        Task UpdateById(IEnumerable<Ticket> tickets);

        Task DeleteById(string id);
    }
}
