using qwitix_api.Core.Entities;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<ITransaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        );

        Task<ITransaction> GetByTransactionId(string id);
    }
}
