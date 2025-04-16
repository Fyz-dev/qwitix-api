using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using qwitix_api.Core.Models;
using qwitix_api.Core.Processors;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Processors
{
    public class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtsettings;

        public AuthTokenProcessor(
            IOptions<JwtSettings> jwtsettings,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtsettings = jwtsettings.Value;
        }

        public (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.Secret));

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.FullName),
                new Claim("user_id", user.Id.ToString()),
            };

            var expires = DateTime.UtcNow.AddMinutes(
                _jwtsettings.AccessTokenExpirationTimeInMinutes
            );

            var token = new JwtSecurityToken(
                issuer: _jwtsettings.Issuer,
                audience: _jwtsettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return (jwtToken, expires);
        }

        public (string refreshToken, DateTime expiresAtUtc) GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);
            var expires = DateTime.UtcNow.AddDays(_jwtsettings.RefreshTokenExpirationTimeInMinutes);

            return (refreshToken, expires);
        }

        public void WriteAuthTokenAsHttpOnlyCookie(
            string cookieName,
            string token,
            DateTime expiration
        )
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(
                cookieName,
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = expiration,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                }
            );
        }
    }
}
