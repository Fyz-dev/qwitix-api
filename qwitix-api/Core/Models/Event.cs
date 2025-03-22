using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class Event : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("organizer_id")]
        public string OrganizerId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonRequired]
        [BsonElement("category")]
        public string Category { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("status")]
        public EventStatus Status { get; set; } = EventStatus.Draft;

        [BsonRequired]
        [BsonElement("venue")]
        public Venue Venue { get; set; } = null!;

        [BsonRequired]
        [BsonElement("start_date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [BsonRequired]
        [BsonElement("end_date")]
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
    }
}
