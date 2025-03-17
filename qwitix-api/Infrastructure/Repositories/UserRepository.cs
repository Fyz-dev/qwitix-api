using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.UsersCollectionName) { }

        public Task Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
