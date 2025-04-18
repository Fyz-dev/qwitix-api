using qwitix_api.Core.Models;
using qwitix_api.Core.Services.OrganizerService.DTOs;

namespace qwitix_api.Core.Mappers.OrganizerMappers
{
    public class ResponseOrginizerMapper : BaseMapper<ResponseOrganizerDTO, Organizer>
    {
        public override Organizer ToEntity(ResponseOrganizerDTO dto)
        {
            throw new NotImplementedException();
        }

        public override ResponseOrganizerDTO ToDto(Organizer entity)
        {
            return new ResponseOrganizerDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                Bio = entity.Bio,
                ImageUrl = entity.ImageUrl,
                IsVerified = entity.IsVerified,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
    }
}
