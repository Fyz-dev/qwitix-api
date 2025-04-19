using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Services.UserService.DTOs
{
    public record UpdateUserDTO
    {
        public string? FullName { get; init; }
        public string? Email { get; init; }
        public string? ImageUrl { get; init; }
    }
}
