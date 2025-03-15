using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Entities
{
    public interface ITransaction : IBaseEntity
    {
        public string UserId { get; set; }

        public string TicketId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public TransactionStatus Status { get; set; }

        public string StripePaymentId { get; set; }
    }
}
