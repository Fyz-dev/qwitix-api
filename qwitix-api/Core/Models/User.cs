using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class User : BaseModel
    {
        [BsonElement("refresh_token")]
        public string? RefreshToken { get; set; }

        [BsonElement("refresh_token_expires")]
        public DateTime? RefreshTokenExpires { get; set; }

        [BsonRequired]
        [BsonElement("google_id")]
        public string GoogleId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("stripe_customer_id")]
        public string StripeCustomerId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("fullName")]
        public string FullName { get; set; } = null!;

        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("role")]
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
