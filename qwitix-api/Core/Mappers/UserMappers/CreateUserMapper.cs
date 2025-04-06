using qwitix_api.Core.Models;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Core.Mappers.UserMappers
{
    public class CreateUserMapper : BaseMapper<CreateUserDTO, User>
    {
        public override CreateUserDTO ToDto(User entity)
        {
            throw new NotImplementedException();
        }

        public override User ToEntity(CreateUserDTO dto)
        {
            return new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role,
            };
        }
    }
}
