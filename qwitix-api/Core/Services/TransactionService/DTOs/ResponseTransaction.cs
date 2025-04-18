using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.DTOs;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Core.Services.TransactionService.DTOs
{
    public record ResponseTransactionDTO : ResponseBaseDTO
    {
        public required string UserId { get; set; }

        public required List<TicketPurchaseDTO> Tickets { get; set; }

        public required string Currency { get; set; }

        public required TransactionStatus Status { get; set; }
    }
}
