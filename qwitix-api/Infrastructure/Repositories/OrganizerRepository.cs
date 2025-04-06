using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class OrganizerRepository : MongoRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.OrganizersCollectionName) { }

        public async Task Create(Organizer organizer)
        {
            await _collection.InsertOneAsync(organizer);
        }

        public async Task<IEnumerable<Organizer>> GetAll(int offset, int limit)
        {
            var filter = Builders<Organizer>.Filter.Empty;

            return await _collection.Find(filter).Skip(offset).Limit(limit).ToListAsync();
        }

        public async Task<Organizer?> GetById(string id)
        {
            var filter = Builders<Organizer>.Filter.Eq(o => o.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateById(string id, Organizer organizer)
        {
            organizer.UpdatedAt = DateTime.UtcNow;

            var filter = Builders<Organizer>.Filter.Eq(o => o.Id, id);

            var result = await _collection.ReplaceOneAsync(filter, organizer);

            if (result.ModifiedCount == 0)
                throw new Exception("Organizer not found or no changes made.");
        }
    }
}
