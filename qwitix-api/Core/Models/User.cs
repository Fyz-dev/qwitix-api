﻿using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;

namespace qwitix_api.Core.Models
{
    public class User : BaseModel
    {
        private string _stripeCustomerId = null!;
        private string _fullName = null!;
        private string _email = null!;

        [BsonElement("refresh_token")]
        public string? RefreshToken { get; set; }

        [BsonElement("refresh_token_expires")]
        public DateTime? RefreshTokenExpires;

        [BsonRequired]
        [BsonElement("stripe_customer_id")]
        public string StripeCustomerId
        {
            get => _stripeCustomerId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("StripeCustomerId cannot be empty.");

                _stripeCustomerId = value;
            }
        }

        [BsonRequired]
        [BsonElement("full_name")]
        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("FullName cannot be empty.");

                _fullName = value;
            }
        }

        [BsonElement("img_url")]
        public string? ImageUrl { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Email cannot be empty.");

                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                if (!emailRegex.IsMatch(value))
                    throw new ValidationException(
                        "Email must follow the standard format (example@domain.com)."
                    );

                _email = value;
            }
        }
    }
}
