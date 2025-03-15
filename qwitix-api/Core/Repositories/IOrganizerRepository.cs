using qwitix_api.Core.Entities;

namespace qwitix_api.Core.Repositories
{
    public interface IOrganizerRepository
    {
        Task Create(IOrganizer organizer);

        Task<IEnumerable<IOrganizer>> GetAll(int offset, int limit);

        Task<IOrganizer> GetById(string id);

        Task UpdateById(string id, IOrganizer user);
    }
}
