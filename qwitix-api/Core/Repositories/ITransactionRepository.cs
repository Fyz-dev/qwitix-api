using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        );

        Task<Transaction> GetByTransactionId(string id);
    }
}
