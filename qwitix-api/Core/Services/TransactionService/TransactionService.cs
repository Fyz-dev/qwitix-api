using qwitix_api.Core.Dispatcher;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Core.Services.TransactionService
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactonRepository;
        private readonly IMapper<ResponseTransactionDTO, Transaction> _responseTransactionMapper;

        public TransactionService(
            ITransactionRepository transactonRepository,
            IUserRepository userRepository,
            IMapper<ResponseTransactionDTO, Transaction> responseTransactionMapper
        )
        {
            _transactonRepository = transactonRepository;
            _responseTransactionMapper = responseTransactionMapper;
        }

        public async Task<IEnumerable<ResponseTransactionDTO>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            var transactions = await _transactonRepository.GetByUserId(
                userId,
                offset,
                limit,
                status
            );

            return _responseTransactionMapper.ToDtoList(transactions);
        }

        public async Task<ResponseTransactionDTO> GetByTransactionId(string id)
        {
            var transaction =
                await _transactonRepository.GetByTransactionId(id)
                ?? throw new NotFoundException("Transaction not found.");

            return _responseTransactionMapper.ToDto(transaction);
        }
    }
}
