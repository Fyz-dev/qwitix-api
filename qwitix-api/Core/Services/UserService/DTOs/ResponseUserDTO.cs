using qwitix_api.Core.Enums;
using qwitix_api.Core.Services.DTOs;

namespace qwitix_api.Core.Services.UserService.DTOs
{
    public record ResponseUserDTO : ResponseBaseDTO
    {
        public required string StripeCustomerId { get; init; }

        public required string FullName { get; init; }

        public required string Email { get; init; }

        public string? Token { get; set; }

        public string? ImageUrl { get; init; }
    }
}
