using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories.EventRepository
{
    public interface IEventRepository
    {
        Task Create(Event eventModel);

        Task<IEnumerable<Event>> GetAll(string organizerId, int offset, int limit);

        Task<Event> GetById(string id);

        Task UpdateById(string id, Event eventModel);

        Task DeleteById(string id);
    }
}
