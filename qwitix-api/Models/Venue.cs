using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Models
{
    public class Venue
    {
        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonElement("city")]
        public string City { get; set; } = null!;

        [BsonElement("state")]
        public string State { get; set; } = null!;

        [BsonElement("zip")]
        public string Zip { get; set; } = null!;
    }
}
