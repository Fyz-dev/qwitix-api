using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.DTOs;

namespace qwitix_api.Core.Services.TransactionService.DTOs
{
    public record ResponseTransactionDTO : ResponseBaseDTO
    {
        public required string UserId { get; set; }

        public required string TicketId { get; set; }

        public required decimal Amount { get; set; }

        public required string Currency { get; set; }

        public required TransactionStatus Status { get; set; }
    }
}
