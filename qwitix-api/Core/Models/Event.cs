using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Models
{
    public class Event : BaseModel
    {
        private string _organizerId = null!;
        private string _title = null!;
        private string? _description;
        private string _category = null!;
        private Venue _venue = null!;
        private DateTime _startDate = DateTime.UtcNow;
        private DateTime _endDate = DateTime.UtcNow;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("organizer_id")]
        public string OrganizerId
        {
            get => _organizerId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("OrganizerId cannot be empty.");

                _organizerId = value;
            }
        }

        [BsonRequired]
        [BsonElement("title")]
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty.");

                if (value.Length > 250)
                    throw new ArgumentException("Title cannot exceed 250 characters.");

                _title = value;
            }
        }

        [BsonElement("description")]
        public string? Description
        {
            get => _description;
            set
            {
                if (value?.Length > 10000)
                    throw new ArgumentException("Description cannot exceed 10000 characters.");

                _description = value;
            }
        }

        [BsonRequired]
        [BsonElement("category")]
        public string Category
        {
            get => _category;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Category cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException("Category cannot exceed 100 characters.");

                _category = value;
            }
        }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("status")]
        public EventStatus Status { get; set; } = EventStatus.Draft;

        [BsonRequired]
        [BsonElement("venue")]
        public Venue Venue
        {
            get => _venue;
            set
            {
                if (value == null)
                    throw new ArgumentException("Venue cannot be null.");

                _venue = value;
            }
        }

        [BsonRequired]
        [BsonElement("start_date")]
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value < DateTime.UtcNow)
                    throw new ArgumentException(
                        "StartDate cannot be earlier than the current date."
                    );

                _startDate = value;
            }
        }

        [BsonRequired]
        [BsonElement("end_date")]
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value < StartDate)
                    throw new ArgumentException("EndDate cannot be earlier than StartDate.");
                _endDate = value;
            }
        }
    }
}
