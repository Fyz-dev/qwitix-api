namespace qwitix_api.Core.Services.OrganizerService.DTOs
{
    public record UpdateOrganizerDTO
    {
        public string? Name { get; init; }

        public string? Bio { get; init; }

        public string? ImageUrl { get; init; }

        public bool? IsVerified { get; init; }
    }
}
