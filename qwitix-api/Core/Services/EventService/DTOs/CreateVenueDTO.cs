namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record CreateVenueDTO
    {
        public required string Name { get; init; }

        public required string Address { get; init; }

        public required string City { get; init; }

        public string? State { get; init; }

        public string? Zip { get; set; }
    }
}
