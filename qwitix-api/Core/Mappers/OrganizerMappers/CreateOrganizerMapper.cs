using qwitix_api.Core.Models;
using qwitix_api.Core.Services.OrganizerService.DTOs;

namespace qwitix_api.Core.Mappers.OrganizerMappers
{
    public class CreateOrganizerMapper : BaseMapper<CreateOrganizerDTO, Organizer>
    {
        public override Organizer ToEntity(CreateOrganizerDTO dto)
        {
            return new Organizer
            {
                UserId = dto.UserId,
                Name = dto.Name,
                Bio = dto.Bio,
                ImageUrl = dto.ImageUrl,
            };
        }

        public override CreateOrganizerDTO ToDto(Organizer entity)
        {
            throw new NotImplementedException();
        }
    }
}
