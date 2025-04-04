using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class Transaction : BaseModel
    {
        private string _userId = null!;
        private string _ticketId = null!;
        private int _amount;
        private string _currency = "USD";
        private string _stripePaymentId = string.Empty;

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
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ticket_id")]
        public string TicketId
        {
            get => _ticketId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("TicketId is required.");

                _ticketId = value;
            }
        }

        [BsonRequired]
        [BsonElement("amount")]
        public int Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Amount cannot be negative.");

                _amount = value;
            }
        }

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
        [BsonElement("stripe_payment_id")]
        public string StripePaymentId
        {
            get => _stripePaymentId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("StripePaymentId is required.");

                _stripePaymentId = value;
            }
        }
    }
}
