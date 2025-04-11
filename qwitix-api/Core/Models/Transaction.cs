using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class Transaction : BaseModel
    {
        private string _userId = null!;
        private string _currency = "USD";
        private string _stripeCheckoutSession = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId
        {
            get => _userId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("UserId is required.");

                _userId = value;
            }
        }

        [BsonRequired]
        [BsonElement("tickets")]
        public List<TicketPurchase> Tickets { get; set; } = new List<TicketPurchase>();

        [BsonRequired]
        [BsonElement("currency")]
        public string Currency
        {
            get => _currency;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Currency is required.");

                _currency = value;
            }
        }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("status")]
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        [BsonRequired]
        [BsonElement("stripe_checkout_session")]
        public string StripeCheckoutSession
        {
            get => _stripeCheckoutSession;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("StripeCheckoutSession is required.");

                _stripeCheckoutSession = value;
            }
        }

        [BsonElement("stripe_payment_intent_id")]
        public string? StripePaymentIntentId = null;
    }
}
