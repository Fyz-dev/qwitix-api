using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Models;
using qwitix_api.Core.Services.AccountService;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly LinkGenerator _linkGenerator;

        public AccountController(AccountService accountService, LinkGenerator linkGenerator)
        {
            _accountService = accountService;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["REFRESH_TOKEN"];

                await _accountService.RefreshTokenAsync(refreshToken);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("login/google")]
        public IActionResult LoginWithGoogle([FromQuery] string returnUrl)
        {
            var callbackUrl =
                _linkGenerator.GetPathByName(HttpContext, "GoogleLoginCallback")
                + "?returnUrl="
                + returnUrl;

            var properties = new AuthenticationProperties { RedirectUri = callbackUrl };

            return Challenge(properties, "Google");
        }

        [HttpGet("login/google/callback", Name = "GoogleLoginCallback")]
        public async Task<IActionResult> GoogleLoginCallback([FromQuery] string returnUrl)
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return Unauthorized();

            await _accountService.LoginWithGoogleAsync(result.Principal);

            return Redirect(returnUrl);
        }
    }
}
