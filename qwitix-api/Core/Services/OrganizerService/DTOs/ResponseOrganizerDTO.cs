using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.DTOs;

namespace qwitix_api.Core.Services.OrganizerService.DTOs
{
    public record ResponseOrganizerDTO : ResponseBaseDTO
    {
        public required string UserId { get; set; }

        public required string Name { get; set; }

        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public required bool IsVerified { get; set; }
    }
}
