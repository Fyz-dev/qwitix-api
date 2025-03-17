using Microsoft.Extensions.Options;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class OrganizerRepository : MongoRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.OrganizersCollectionName) { }

        public Task Create(Organizer organizer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Organizer>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
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
