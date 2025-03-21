using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Venue
    {
        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonRequired]
        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonRequired]
        [BsonElement("city")]
        public string City { get; set; } = null!;

        [BsonElement("state")]
        public string? State { get; set; }

        [BsonElement("zip")]
        public string? Zip { get; set; }
    }
}
