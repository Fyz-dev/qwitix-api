using System.Security.Claims;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;
using qwitix_api.Core.Processors;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Service.StripeService;

namespace qwitix_api.Core.Services.AccountService
{
    public class AccountService
    {
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly IUserRepository _userRepository;
        private readonly StripeService _stripeService;

        public AccountService(
            IAuthTokenProcessor authTokenProcessor,
            IUserRepository userRepository,
            StripeService stripeService
        )
        {
            _authTokenProcessor = authTokenProcessor;
            _userRepository = userRepository;
            _stripeService = stripeService;
        }

        public async Task RefreshTokenAsync(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new RefreshTokenException("Refresh token is missing.");

            var user =
                await _userRepository.GetUserByRefreshToken(refreshToken)
                ?? throw new RefreshTokenException("Unable to retrieve user for refresh token");

            if (user.RefreshTokenExpires < DateTime.UtcNow)
                throw new RefreshTokenException("Refresh token is expired.");

            await SetAuthTokensAsync(user);
        }

        public async Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal is null)
                throw new ExternalLoginProviderException("Google", "ClaimsPrincipal is null");

            string email =
                claimsPrincipal.FindFirstValue(ClaimTypes.Email)
                ?? throw new ExternalLoginProviderException("Google", "Email is null");

            string name = string.Join(
                    " ",
                    claimsPrincipal.FindFirstValue(ClaimTypes.GivenName),
                    claimsPrincipal.FindFirstValue(ClaimTypes.Surname)
                )
                .Trim();

            User? user = await _userRepository.GetUserByEmail(email);

            if (user is null)
            {
                var customer = await _stripeService.CreateCustomerAsync(name, email);

                user = new User
                {
                    FullName = name,
                    Email = email,
                    StripeCustomerId = customer.Id,
                };

                await _userRepository.Create(user);
            }

            await SetAuthTokensAsync(user);
        }

        private async Task SetAuthTokensAsync(User user)
        {
            var (jwtToken, jwtExpirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
            var (refreshToken, refreshExpirationDateInUtc) =
                _authTokenProcessor.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = refreshExpirationDateInUtc;

            await _userRepository.UpdateById(user.Id, user);

            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie(
                "ACCESS_TOKEN",
                jwtToken,
                jwtExpirationDateInUtc
            );
            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie(
                "REFRESH_TOKEN",
                refreshToken,
                refreshExpirationDateInUtc
            );
        }
    }
}
