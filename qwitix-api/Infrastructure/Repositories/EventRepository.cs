using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class EventRepository : MongoRepository<Event>, IEventRepository
    {
        public EventRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.EventsCollectionName) { }

        public async Task Create(Event eventModel)
        {
            await _collection.InsertOneAsync(eventModel);
        }

        public Task DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetAll(string organizerId, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(string id, Event eventModel)
        {
            throw new NotImplementedException();
        }
    }
}
