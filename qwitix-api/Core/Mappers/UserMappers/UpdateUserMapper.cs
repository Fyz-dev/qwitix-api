using qwitix_api.Core.Models;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Core.Mappers.UserMappers
{
    public class UpdateUserMapper : BaseMapper<UpdateUserDTO, User>
    {
        public override User ToEntity(UpdateUserDTO dto)
        {
            return new User { FullName = dto.FullName, Email = dto.Email };
        }

        public override UpdateUserDTO ToDto(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
