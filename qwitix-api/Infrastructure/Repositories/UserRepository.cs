using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class UserRepository(IOptions<DatabaseSettings> databaseSettings)
        : MongoRepository<User>(databaseSettings, databaseSettings.Value.UsersCollectionName),
            IUserRepository
    {
        public async Task Create(User user)
        {
            await _collection.InsertOneAsync(user);
        }

        public async Task<User?> GetById(string id)
        {
            return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByStripeCustomerId(string id)
        {
            return await _collection.Find(u => u.StripeCustomerId == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByRefreshToken(string refreshToken)
        {
            return await _collection
                .Find(u => u.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateById(string id, User user)
        {
            user.UpdatedAt = DateTime.UtcNow;

            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var result = await _collection.ReplaceOneAsync(filter, user);

            if (result.ModifiedCount == 0)
                throw new NotFoundException("User not found or no changes made.");
        }
    }
}
