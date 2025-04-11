using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Exceptions;

namespace qwitix_api.Core.Models
{
    public class Organizer : BaseModel
    {
        private string _userId = null!;
        private string _name = null!;
        private string? _bio;
        private string? _imageUrl;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId
        {
            get => _userId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("UserId cannot be empty.");

                _userId = value;
            }
        }

        [BsonRequired]
        [BsonElement("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Name cannot be empty.");

                if (value.Length > 250)
                    throw new ValidationException("Name cannot be longer than 250 characters.");

                _name = value;
            }
        }

        [BsonRequired]
        [BsonElement("bio")]
        public string? Bio
        {
            get => _bio;
            set
            {
                if (value?.Length > 2500)
                    throw new ValidationException("Bio cannot be longer than 2500 characters.");

                _bio = value;
            }
        }

        [BsonElement("image_url")]
        public string? ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value is not null)
                {
                    var urlRegex = new Regex(
                        @"^(https?|ftp)://[^\s/$.?#].[^\s]*$",
                        RegexOptions.IgnoreCase
                    );

                    if (!urlRegex.IsMatch(value))
                        throw new ValidationException("ImageUrl must be a valid URL.");
                }

                _imageUrl = value;
            }
        }

        [BsonElement("is_verified")]
        public bool IsVerified { get; set; } = false;
    }
}
