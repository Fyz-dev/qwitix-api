using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class TicketRepository : MongoRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.TicketsCollectionName) { }

        public async Task Create(Ticket ticket)
        {
            await _collection.InsertOneAsync(ticket);
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.Id, id);

            var result = await _collection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
                throw new Exception("Ticket not found.");
        }

        public async Task<IEnumerable<Ticket>> GetAll(string eventId, int offset, int limit)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.EventId, eventId);

            return await _collection.Find(filter).Skip(offset).Limit(limit).ToListAsync();
        }

        public async Task<Ticket?> GetById(string id)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateById(string id, Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.Id, id);

            var result = await _collection.ReplaceOneAsync(filter, ticket);

            if (result.ModifiedCount == 0)
                throw new Exception("Ticket not found or no changes made.");
        }
    }
}
