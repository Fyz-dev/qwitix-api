using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Exceptions;

namespace qwitix_api.Core.Models
{
    public class Ticket : BaseModel
    {
        private string _eventId = null!;
        private string _name = null!;
        private string? _details;
        private decimal _price;
        private int _quantity;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("event_id")]
        public string EventId
        {
            get => _eventId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("EventId is required.");

                _eventId = value;
            }
        }

        [BsonRequired]
        [BsonElement("stripe_price_id")]
        public string? StripePriceId;

        [BsonRequired]
        [BsonElement("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Name is required.");

                if (value.Length > 100)
                    throw new ValidationException("Name cannot be longer than 100 characters.");

                _name = value;
            }
        }

        [BsonElement("details")]
        public string? Details
        {
            get => _details;
            set
            {
                if (value?.Length > 800)
                    throw new ValidationException("Details cannot be longer than 800 characters.");

                _details = value;
            }
        }

        [BsonRequired]
        [BsonElement("price")]
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ValidationException("Price cannot be negative.");

                _price = value;
            }
        }

        [BsonRequired]
        [BsonElement("quantity")]
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value <= 0)
                    throw new ValidationException("Quantity cannot be negative or  zero.");

                _quantity = value;
            }
        }
    }
}
