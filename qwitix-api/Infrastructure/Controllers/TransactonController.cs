﻿using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.TransactionService;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TransactonController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactonController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("transactions")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseTransactionDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            IEnumerable<ResponseTransactionDTO> transactions =
                await _transactionService.GetByUserId(userId, offset, limit, status);

            return Ok(transactions);
        }

        [HttpGet("transaction/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTransactionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByTransactionId(string id)
        {
            ResponseTransactionDTO transaction = await _transactionService.GetByTransactionId(id);

            return Ok(transaction);
        }
    }
}
