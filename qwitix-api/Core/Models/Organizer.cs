using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Organizer : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonRequired]
        [BsonElement("bio")]
        public string Bio { get; set; } = null!;

        [BsonElement("image_url")]
        public string? ImageUrl { get; set; }

        [BsonElement("is_verified")]
        public bool IsVerified { get; set; } = false;
    }
}
