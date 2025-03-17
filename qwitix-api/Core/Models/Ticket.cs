using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Ticket : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("event_id")]
        public string EventId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("details")]
        public string Details { get; set; } = null!;

        [BsonRequired]
        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonRequired]
        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("sold")]
        public int Sold { get; set; } = 0;
    }
}
