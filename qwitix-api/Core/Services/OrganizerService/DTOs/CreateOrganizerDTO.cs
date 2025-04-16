namespace qwitix_api.Core.Services.OrganizerService.DTOs
{
    public record CreateOrganizerDTO
    {
        public required string Name { get; init; }

        public string? Bio { get; init; }

        public string? ImageUrl { get; init; }
    }
}
