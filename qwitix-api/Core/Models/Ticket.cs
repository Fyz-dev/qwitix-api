using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Ticket : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("event_id")]
        [Required(ErrorMessage = "EventId is required.")]
        public string EventId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("name")]
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;

        [BsonElement("details")]
        [Required(ErrorMessage = "Details are required.")]
        [MaxLength(800, ErrorMessage = "Details cannot be longer than 800 characters.")]
        public string Details { get; set; } = null!;

        [BsonRequired]
        [BsonElement("price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public decimal Price { get; set; }

        [BsonRequired]
        [BsonElement("quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [BsonElement("sold")]
        [Range(0, int.MaxValue, ErrorMessage = "Sold cannot be negative.")]
        [CustomValidation(typeof(Ticket), nameof(ValidateSold))]
        public int Sold { get; set; } = 0;

        private static ValidationResult? ValidateSold(int sold, ValidationContext context)
        {
            var instance = (Ticket)context.ObjectInstance;

            if (sold > instance.Quantity)
                return new ValidationResult("Sold cannot be greater than Quantity.");

            return ValidationResult.Success;
        }
    }
}
