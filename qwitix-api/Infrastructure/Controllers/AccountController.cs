using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using qwitix_api.Core.Services.AccountService;
using qwitix_api.Core.Services.DTOs;
using qwitix_api.Core.Services.OrganizerService.DTOs;
using qwitix_api.Core.Services.UserService.DTOs;

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

        [HttpGet(Name = "GetAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseAccountDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById()
        {
            var userId = User.FindFirst("user_id")?.Value;
            var accessToken = Request.Cookies["ACCESS_TOKEN"];

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            ResponseUserDTO user = await _accountService.GetById(userId);

            ResponseAccountDTO response = new() { User = user, Token = accessToken };

            return Ok(response);
        }

        [HttpGet("organizer", Name = "GetOrganizerAccount")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseOrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            ResponseOrganizerDTO organizer = await _accountService.GetOrganizerById(userId);

            return Ok(organizer);
        }

        [HttpPost("refresh", Name = "UpdateRefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["REFRESH_TOKEN"];

            await _accountService.RefreshTokenAsync(refreshToken);

            return Ok();
        }

        [HttpPost("logout", Name = "Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UnixEpoch,
                Secure = true,
                SameSite = SameSiteMode.None,
            };

            Response.Cookies.Delete("ACCESS_TOKEN", cookieOptions);
            Response.Cookies.Delete("REFRESH_TOKEN", cookieOptions);

            return Ok();
        }

        [HttpGet("registration/google", Name = "GetGoogleLoginUrl")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UrlResponseDTO))]
        public IActionResult GetGoogleRegistrationUrl([Required] string returnUrl)
        {
            var loginUrl = Url.Link("GoogleLogin", new { returnUrl });

            return Ok(new UrlResponseDTO { Url = loginUrl! });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("login/google", Name = "GoogleLogin")]
        public IActionResult LoginWithGoogle([FromQuery] string returnUrl)
        {
            var callbackUrl =
                _linkGenerator.GetPathByName(HttpContext, "GoogleLoginCallback")
                + "?returnUrl="
                + returnUrl;

            var properties = new AuthenticationProperties { RedirectUri = callbackUrl };

            return Challenge(properties, "Google");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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
