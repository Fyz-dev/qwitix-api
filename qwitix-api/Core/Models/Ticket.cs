using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Ticket : BaseModel
    {
        private string _eventId = null!;
        private string _name = null!;
        private string _details = null!;
        private decimal _price;
        private int _quantity;
        private int _sold;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("event_id")]
        public string EventId
        {
            get => _eventId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("EventId is required.");

                _eventId = value;
            }
        }

        [BsonRequired]
        [BsonElement("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name is required.");

                if (value.Length > 100)
                    throw new ArgumentException("Name cannot be longer than 100 characters.");

                _name = value;
            }
        }

        [BsonElement("details")]
        public string Details
        {
            get => _details;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Details are required.");

                if (value.Length > 800)
                    throw new ArgumentException("Details cannot be longer than 800 characters.");

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
                    throw new ArgumentException("Price cannot be negative.");

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
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");

                if (_sold > value)
                    throw new ArgumentException("Sold cannot be greater than Quantity.");

                _quantity = value;
            }
        }

        [BsonElement("sold")]
        public int Sold
        {
            get => _sold;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Sold cannot be negative.");

                if (value > Quantity)
                    throw new ArgumentException("Sold cannot be greater than Quantity.");

                _sold = value;
            }
        }
    }
}
