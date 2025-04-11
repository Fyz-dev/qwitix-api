using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task Create(Transaction transaction);

        Task<Transaction?> GetByTransactionId(string id);

        Task<Transaction?> GetByCheckoutSessionId(string id);

        Task<IEnumerable<Transaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        );

        Task<Dictionary<string, int>> GetTotalSoldQuantityForTickets(IEnumerable<string> ticketIds);

        Task UpdateById(string id, Transaction transaction);
    }
}
