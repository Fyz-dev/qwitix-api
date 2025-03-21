namespace qwitix_api.Infrastructure.Configs
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public int AccessTokenExpirationTimeInMinutes { get; set; } = 15;

        public int RefreshTokenExpirationTimeInMinutes { get; set; } = 10080;
    }
}
