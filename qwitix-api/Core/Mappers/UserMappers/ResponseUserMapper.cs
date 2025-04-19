using qwitix_api.Core.Models;
using qwitix_api.Core.Services.UserService.DTOs;

namespace qwitix_api.Core.Mappers.UserMappers
{
    public class ResponseUserMapper : BaseMapper<ResponseUserDTO, User>
    {
        public override ResponseUserDTO ToDto(User entity)
        {
            return new ResponseUserDTO
            {
                Id = entity.Id,
                StripeCustomerId = entity.StripeCustomerId,
                FullName = entity.FullName,
                ImageUrl = entity.ImageUrl,
                Email = entity.Email,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }

        public override User ToEntity(ResponseUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
