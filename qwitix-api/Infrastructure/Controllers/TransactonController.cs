using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.TransactionService;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactonController
    {
        private readonly TransactionService _transactionService;

        public TransactonController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("/transactions")]
        public async Task<IEnumerable<ResponseTransaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            return await _transactionService.GetByUserId(userId, offset, limit, status);
        }

        [HttpGet("/transaction/{id}")]
        public async Task<ResponseTransaction> GetByTransactionId(string id)
        {
            return await _transactionService.GetByTransactionId(id);
        }
    }
}
