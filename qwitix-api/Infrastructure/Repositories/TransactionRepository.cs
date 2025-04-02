using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class TransactionRepository : MongoRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.TransactionsCollectionName) { }

        public async Task<Transaction?> GetByTransactionId(string id)
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.UserId, userId);

            if (status.HasValue)
                filter = Builders<Transaction>.Filter.And(
                    filter,
                    Builders<Transaction>.Filter.Eq(t => t.Status, status.Value)
                );

            return await _collection.Find(filter).Skip(offset).Limit(limit).ToListAsync();
        }
    }
}
