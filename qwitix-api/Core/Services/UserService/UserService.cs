using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Core.Services.UserService
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(CreateUserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateById(string id, UpdateUserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
