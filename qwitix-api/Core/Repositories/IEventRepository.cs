using qwitix_api.Core.Entities;

namespace qwitix_api.Core.Repositories.EventRepository
{
    public interface IEventRepository<TVenue>
        where TVenue : IVenue
    {
        Task Create(IEvent<TVenue> eventDTO);

        Task<IEnumerable<IEvent<TVenue>>> GetAll(string organizerId, int offset, int limit);

        Task<IEvent<TVenue>> GetById(string id);

        Task UpdateById(string id, IEvent<TVenue> eventDTO);

        Task DeleteById(string id);
    }
}
