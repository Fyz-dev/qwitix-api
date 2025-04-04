using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
                    throw new ArgumentException("UserId cannot be empty.");

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
                    throw new ArgumentException("Name cannot be empty.");

                if (value.Length > 250)
                    throw new ArgumentException("Name cannot be longer than 250 characters.");

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
                    throw new ArgumentException("Bio cannot be longer than 2500 characters.");

                _bio = value;
            }
        }

        [BsonElement("image_url")]
        public string? ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ImageUrl cannot be empty.");

                var urlRegex = new Regex(
                    @"^(https?|ftp)://[^\s/$.?#].[^\s]*$",
                    RegexOptions.IgnoreCase
                );

                if (!urlRegex.IsMatch(value))
                    throw new ArgumentException("ImageUrl must be a valid URL.");

                _imageUrl = value;
            }
        }

        [BsonElement("is_verified")]
        public bool IsVerified { get; set; } = false;
    }
}
