using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class Transaction : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ticket_id")]
        [Required(ErrorMessage = "TicketId is required.")]
        public string TicketId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public int Amount { get; set; }

        [BsonRequired]
        [BsonElement("currency")]
        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; } = "USD";

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("status")]
        [Required(ErrorMessage = "Status is required.")]
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        [BsonRequired]
        [BsonElement("stripe_payment_id")]
        [Required(ErrorMessage = "StripePaymentId is required.")]
        public string StripePaymentId { get; set; } = string.Empty;
    }
}
