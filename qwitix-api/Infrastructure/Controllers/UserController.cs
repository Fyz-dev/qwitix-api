using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.UserService;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/user/{id}")]
        public async Task Create(CreateUserDTO userDTO)
        {
            await _userService.Create(userDTO);
        }

        [HttpGet("/user/{id}")]
        public async Task<ResponseDTO> GetById(string id)
        {
            return await _userService.GetById(id);
        }

        [HttpPatch("/user/{id}")]
        public async Task UpdateById(string id, UpdateUserDTO userDTO)
        {
            await _userService.UpdateById(id, userDTO);
        }
    }
}
