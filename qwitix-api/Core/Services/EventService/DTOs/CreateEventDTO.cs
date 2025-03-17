namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record CreateEventDTO
    {
        public required string OrganizerId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public required CreateVenueDTO Venue { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }
    }
}
