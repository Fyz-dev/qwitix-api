using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Core.Services.UserService
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper<ResponseUserDTO, User> _responseUserMapper;

        public UserService(
            IUserRepository userRepository,
            IMapper<ResponseUserDTO, User> responseUserMapper
        )
        {
            _userRepository = userRepository;
            _responseUserMapper = responseUserMapper;
        }

        public async Task<ResponseUserDTO> GetById(string id)
        {
            var user =
                await _userRepository.GetById(id)
                ?? throw new NotFoundException($"User not found.");

            return _responseUserMapper.ToDto(user);
        }

        public async Task UpdateById(string id, UpdateUserDTO userDTO)
        {
            var user =
                await _userRepository.GetById(id) ?? throw new NotFoundException("User not found.");

            PatchHelper.ApplyPatch(userDTO, user);

            await _userRepository.UpdateById(id, user);
        }
    }
}
