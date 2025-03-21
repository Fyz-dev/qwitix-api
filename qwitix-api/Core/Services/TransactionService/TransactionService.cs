using qwitix_api.Core.Enums;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Core.Services.TransactionService
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactonRepository;

        public TransactionService(ITransactionRepository transactonRepository)
        {
            _transactonRepository = transactonRepository;
        }

        public async Task<IEnumerable<ResponseTransactionDTO>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseTransactionDTO> GetByTransactionId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
