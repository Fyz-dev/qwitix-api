using System.Diagnostics;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class TicketRepository : MongoRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.TicketsCollectionName) { }

        public async Task<Ticket> Create(Ticket ticket)
        {
            await _collection.InsertOneAsync(ticket);

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAll(string eventId)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.EventId, eventId);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<Ticket?> GetById(string id)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ticket>> GetById(params string[] ids)
        {
            var filter = Builders<Ticket>.Filter.In(t => t.Id, ids);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateById(string id, Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.Id, id);

            var result = await _collection.ReplaceOneAsync(filter, ticket);

            if (result.ModifiedCount == 0)
                throw new Exception("Ticket not found or no changes made.");
        }

        public async Task UpdateById(IEnumerable<Ticket> tickets)
        {
            var bulkOps = new List<WriteModel<Ticket>>();

            foreach (var ticket in tickets)
            {
                var filter = Builders<Ticket>.Filter.Eq(t => t.Id, ticket.Id);

                var replaceOne = new ReplaceOneModel<Ticket>(filter, ticket);

                bulkOps.Add(replaceOne);
            }

            if (bulkOps.Count > 0)
            {
                var result = await _collection.BulkWriteAsync(bulkOps);

                if (result.MatchedCount != bulkOps.Count)
                    throw new NotFoundException("Some tickets were not found during bulk update.");
            }
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.Id, id);

            var result = await _collection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
                throw new Exception("Ticket not found.");
        }
    }
}
