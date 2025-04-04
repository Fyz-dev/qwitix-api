using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.UserService;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetById(id);

            return Ok(user);
        }

        [HttpPatch("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, UpdateUserDTO userDTO)
        {
            await _userService.UpdateById(id, userDTO);

            return Ok();
        }
    }
}
