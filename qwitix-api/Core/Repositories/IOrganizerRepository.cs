using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface IOrganizerRepository
    {
        Task Create(Organizer organizer);

        Task<IEnumerable<Organizer>> GetAll(int offset, int limit);

        Task<Organizer?> GetById(string id);

        Task<Organizer?> GetByUserId(string id);

        Task UpdateById(string id, Organizer organizer);
    }
}
