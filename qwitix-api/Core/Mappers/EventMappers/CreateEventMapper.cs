using qwitix_api.Core.Models;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api.Core.Mappers.EventMappers
{
    public class CreateEventMapper : BaseMapper<CreateEventDTO, Event>
    {
        public override Event ToEntity(CreateEventDTO dto)
        {
            return new Event
            {
                OrganizerId = dto.OrganizerId,
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Venue = new Venue
                {
                    Name = dto.Venue.Name,
                    Address = dto.Venue.Address,
                    City = dto.Venue.City,
                    State = dto.Venue.State,
                    Zip = dto.Venue.Zip,
                },
            };
        }

        public override CreateEventDTO ToDto(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
