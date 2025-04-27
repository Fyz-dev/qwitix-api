using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.TransactionService;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("transaction/list", Name = "GetTransactionList")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseTransactionDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId(
            [Required] string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            IEnumerable<ResponseTransactionDTO> transactions =
                await _transactionService.GetByUserId(userId, offset, limit, status);

            return Ok(transactions);
        }

        [HttpGet("transaction/{id}", Name = "GetTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTransactionDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByTransactionId(string id)
        {
            ResponseTransactionDTO transaction = await _transactionService.GetByTransactionId(id);

            return Ok(transaction);
        }
    }
}
