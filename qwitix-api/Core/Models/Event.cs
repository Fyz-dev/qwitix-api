using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class Event : BaseModel
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("organizer_id")]
        [Required(ErrorMessage = "OrganizerId cannot be empty.")]
        public string OrganizerId { get; set; } = null!;

        [BsonRequired]
        [BsonElement("title")]
        [Required(ErrorMessage = "Title cannot be empty.")]
        [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters.")]
        public string Title { get; set; } = null!;

        [BsonElement("description")]
        [MaxLength(10000, ErrorMessage = "Description cannot exceed 10000 characters.")]
        public string? Description { get; set; }

        [BsonRequired]
        [BsonElement("category")]
        [Required(ErrorMessage = "Category cannot be empty.")]
        [MaxLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("status")]
        public EventStatus Status { get; set; } = EventStatus.Draft;

        [BsonRequired]
        [BsonElement("venue")]
        [Required(ErrorMessage = "Venue cannot be empty.")]
        public Venue Venue { get; set; } = null!;

        [BsonRequired]
        [BsonElement("start_date")]
        [Required(ErrorMessage = "StartDate cannot be empty.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Event), nameof(ValidateStartDate))]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [BsonRequired]
        [BsonElement("end_date")]
        [Required(ErrorMessage = "EndDate cannot be empty.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Event), nameof(ValidateEndDate))]
        public DateTime EndDate { get; set; } = DateTime.UtcNow;

        private static ValidationResult? ValidateStartDate(
            DateTime startDate,
            ValidationContext context
        )
        {
            if (startDate < DateTime.UtcNow)
                return new ValidationResult("StartDate cannot be earlier than the current date.");

            return ValidationResult.Success;
        }

        private static ValidationResult? ValidateEndDate(
            DateTime endDate,
            ValidationContext context
        )
        {
            Event? instance = context.ObjectInstance as Event;

            if (instance != null && endDate < instance.StartDate)
                return new ValidationResult("EndDate cannot be earlier than StartDate.");

            return ValidationResult.Success;
        }
    }
}
