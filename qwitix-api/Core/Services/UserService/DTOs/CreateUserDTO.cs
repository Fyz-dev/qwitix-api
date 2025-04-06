using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Services.UserService.DTOs
{
    public record CreateUserDTO
    {
        public required string FullName { get; init; }
        public required string Email { get; init; }
        public required UserRole Role { get; init; }
    }
}
