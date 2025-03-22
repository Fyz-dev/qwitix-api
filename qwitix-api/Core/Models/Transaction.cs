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
        public string UserId { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ticket_id")]
        public string TicketId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("amount")]
        public int Amount { get; set; }

        [BsonRequired]
        [BsonElement("currency")]
        public string Currency { get; set; } = "USD";

        [BsonRepresentation(BsonType.String)]
        [BsonElement("status")]
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        [BsonElement("stripe_payment_id")]
        public string StripePaymentId { get; set; } = string.Empty;
    }
}
