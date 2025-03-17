using Microsoft.Extensions.Options;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class TransactionRepository : MongoRepository<Event>, ITransactionRepository
    {
        public TransactionRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.TransactionsCollectionName) { }

        public Task<Transaction> GetByTransactionId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            throw new NotImplementedException();
        }
    }
}
