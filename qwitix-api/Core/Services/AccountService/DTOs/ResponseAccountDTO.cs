namespace qwitix_api.Core.Services.UserService.DTOs
{
    public record ResponseAccountDTO
    {
        public required ResponseUserDTO User { get; init; }

        public required string Token { get; init; }
    }
}
