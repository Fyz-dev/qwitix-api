using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class TicketPurchase
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ticket_id")]
        public string TicketId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
