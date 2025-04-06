using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class User : BaseModel
    {
        private DateTime? _refreshTokenExpires;
        private string _stripeCustomerId = null!;
        private string _fullName = null!;
        private string _email = null!;

        [BsonElement("refresh_token")]
        public string? RefreshToken { get; set; }

        [BsonElement("refresh_token_expires")]
        public DateTime? RefreshTokenExpires
        {
            get => _refreshTokenExpires;
            set
            {
                if (value.HasValue && value.Value < DateTime.Now)
                    throw new ArgumentException(
                        "RefreshTokenExpires cannot be earlier than the current date."
                    );

                _refreshTokenExpires = value;
            }
        }

        [BsonRequired]
        [BsonElement("stripe_customer_id")]
        public string StripeCustomerId
        {
            get => _stripeCustomerId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("StripeCustomerId cannot be empty.");

                _stripeCustomerId = value;
            }
        }

        [BsonRequired]
        [BsonElement("fullName")]
        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("FullName cannot be empty.");

                _fullName = value;
            }
        }

        [BsonRequired]
        [BsonElement("email")]
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty.");

                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                if (!emailRegex.IsMatch(value))
                    throw new ArgumentException(
                        "Email must follow the standard format (example@domain.com)."
                    );

                _email = value;
            }
        }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("role")]
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
