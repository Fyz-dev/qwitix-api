using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Organizer : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        [Required(ErrorMessage = "UserId cannot be empty")]
        public string UserId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("name")]
        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250, ErrorMessage = "Name cannot be longer than 250 characters")]
        public string Name { get; set; } = null!;

        [BsonRequired]
        [BsonElement("bio")]
        [Required(ErrorMessage = "Bio cannot be empty")]
        [MaxLength(2500, ErrorMessage = "Bio cannot be longer than 2500 characters")]
        public string Bio { get; set; } = null!;

        [BsonElement("image_url")]
        [Url(ErrorMessage = "ImageUrl must be a valid URL")]
        public string? ImageUrl { get; set; }

        [BsonElement("is_verified")]
        public bool IsVerified { get; set; } = false;
    }
}
