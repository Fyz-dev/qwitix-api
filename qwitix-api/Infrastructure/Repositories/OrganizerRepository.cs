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

        public Task<Organizer> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(string id, Organizer organizer)
        {
            throw new NotImplementedException();
        }
    }
}
