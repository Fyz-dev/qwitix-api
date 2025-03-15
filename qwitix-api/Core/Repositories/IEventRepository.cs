using qwitix_api.Core.Entities;

namespace qwitix_api.Core.Repositories.EventRepository
{
    public interface IEventRepository
    {
        Task Create(IEvent<IVenue> eventDTO);

        Task<IEnumerable<IEvent<IVenue>>> GetAll(string organizerId, int offset, int limit);

        Task<IEvent<IVenue>> GetById(string id);

        Task UpdateById(string id, IEvent<IVenue> eventDTO);

        Task DeleteById(string id);
    }
}
