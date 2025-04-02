using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Venue
    {
        [BsonRequired]
        [BsonElement("name")]
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = null!;

        [BsonRequired]
        [BsonElement("address")]
        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; } = null!;

        [BsonRequired]
        [BsonElement("city")]
        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; } = null!;

        [BsonElement("state")]
        [MaxLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
        public string? State { get; set; }

        [BsonElement("zip")]
        [MaxLength(10, ErrorMessage = "Zip cannot exceed 10 characters.")]
        [RegularExpression(
            @"^\d{1,10}(-\d{1,10})?$",
            ErrorMessage = "Zip can only contain numbers and a hyphen."
        )]
        public string? Zip { get; set; }
    }
}
