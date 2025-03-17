namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record ResponseVenueDTO
    {
        public required string Name { get; set; } = null!;
        public required string Address { get; set; } = null!;
        public required string City { get; set; } = null!;
        public string? State { get; set; } = null!;
        public string? Zip { get; set; } = null!;
    }
}
