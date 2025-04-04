using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MongoDB.Bson.Serialization.Attributes;

namespace qwitix_api.Core.Models
{
    public class Venue
    {
        private string _name = null!;
        private string _address = null!;
        private string _city = null!;
        private string? _state;
        private string? _zip;

        [BsonRequired]
        [BsonElement("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name is required.");

                if (value.Length < 3)
                    throw new ArgumentException("Name must be at least 3 characters long.");

                if (value.Length > 255)
                    throw new ArgumentException("Name cannot exceed 255 characters.");

                _name = value;
            }
        }

        [BsonRequired]
        [BsonElement("address")]
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Address is required.");

                if (value.Length > 255)
                    throw new ArgumentException("Address cannot exceed 255 characters.");

                _address = value;
            }
        }

        [BsonRequired]
        [BsonElement("city")]
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City is required.");

                if (value.Length > 100)
                    throw new ArgumentException("City cannot exceed 100 characters.");

                _city = value;
            }
        }

        [BsonElement("state")]
        public string? State
        {
            get => _state;
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentException("State cannot exceed 100 characters.");

                _state = value;
            }
        }

        [BsonElement("zip")]
        public string? Zip
        {
            get => _zip;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City is required.");

                if (value.Length > 10)
                    throw new ArgumentException("Zip cannot exceed 10 characters.");

                var regex = new Regex(@"^\d{1,10}(-\d{1,10})?$");

                if (!regex.IsMatch(value))
                    throw new ArgumentException("Zip can only contain numbers and a hyphen.");

                _zip = value;
            }
        }
    }
}
