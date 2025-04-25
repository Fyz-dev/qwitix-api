using qwitix_api.Core.Models;

namespace qwitix_api.Core.Processors
{
    public interface IAuthTokenProcessor
    {
        (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user);
        (string refreshToken, DateTime expiresAtUtc) GenerateRefreshToken();
        void WriteAuthTokenAsHttpOnlyCookie(
            string cookieName,
            string token,
            DateTime expiration,
            string domain
        );
    }
}
