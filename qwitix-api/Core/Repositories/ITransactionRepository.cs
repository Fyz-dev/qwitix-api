using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByTransactionId(string id);

        Task<IEnumerable<Transaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        );
    }
}
