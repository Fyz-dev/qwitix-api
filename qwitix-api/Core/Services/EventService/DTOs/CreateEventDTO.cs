namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record CreateEventDTO
    {
        public required string OrganizerId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public required string Category { get; set; }

        public required CreateVenueDTO Venue { get; set; }
    }
}
