namespace qwitix_api.Core.Services.EventService.DTOs
{
    public class UpdateVenueDTO
    {
        public string? Name { get; init; }

        public string? Address { get; init; }

        public string? City { get; init; }

        public string? State { get; init; }

        public string? Zip { get; set; }
    }
}
