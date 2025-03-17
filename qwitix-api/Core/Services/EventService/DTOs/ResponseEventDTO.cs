using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;
using qwitix_api.Core.Services.DTOs;

namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record ResponseEventDTO : ResponseBaseDTO
    {
        public required string OrganizerId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public required EventStatus Status { get; set; }

        public required ResponseVenueDTO Venue { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }
    }
}
