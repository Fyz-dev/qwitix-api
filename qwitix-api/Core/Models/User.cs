using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "RefreshTokenExpires cannot be empty.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(User), nameof(ValidateRefreshTokenExpires))]
        public DateTime? RefreshTokenExpires { get; set; }

        [BsonRequired]
        [BsonElement("google_id")]
        [Required(ErrorMessage = "GoogleId cannot be empty.")]
        public string GoogleId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("stripe_customer_id")]
        [Required(ErrorMessage = "StripeCustomerId cannot be empty.")]
        public string StripeCustomerId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("fullName")]
        [Required(ErrorMessage = "FullName cannot be empty.")]
        public string FullName { get; set; } = null!;

        [BsonRequired]
        [BsonElement("email")]
        [EmailAddress(ErrorMessage = "Email must follow the standard format (example@domain.com).")]
        public string Email { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("role")]
        public UserRole Role { get; set; } = UserRole.Customer;

        private static ValidationResult? ValidateRefreshTokenExpires(
            DateTime? refreshTokenExpires,
            ValidationContext context
        )
        {
            if (refreshTokenExpires.HasValue && refreshTokenExpires.Value < DateTime.Now)
                return new ValidationResult(
                    "RefreshTokenExpires cannot be earlier than the current date."
                );

            return ValidationResult.Success;
        }
    }
}
