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

        public async Task DeleteById(string id)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.Id, id);

            var result = await _collection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
                throw new Exception("Event not found.");
        }

        public async Task<IEnumerable<Event>> GetAll(string organizerId, int offset, int limit)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.OrganizerId, organizerId);

            return await _collection.Find(filter).Skip(offset).Limit(limit).ToListAsync();
        }

        public async Task<Event?> GetById(string id)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateById(string id, Event eventModel)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.Id, id);

            var result = await _collection.ReplaceOneAsync(filter, eventModel);

            if (result.ModifiedCount == 0)
                throw new Exception("Event not found or no changes made.");
        }
    }
}
