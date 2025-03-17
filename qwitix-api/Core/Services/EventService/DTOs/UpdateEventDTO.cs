using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record UpdateEventDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public EventStatus? Status { get; set; }

        public UpdateVenueDTO? Venue { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
