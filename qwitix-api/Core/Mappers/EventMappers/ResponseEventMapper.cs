using qwitix_api.Core.Models;
using qwitix_api.Core.Services.EventService.DTOs;

namespace qwitix_api.Core.Mappers.EventMappers
{
    public class ResponseEventMapper : BaseMapper<ResponseEventDTO, Event>
    {
        public override Event ToEntity(ResponseEventDTO dto)
        {
            throw new NotImplementedException();
        }

        public override ResponseEventDTO ToDto(Event entity)
        {
            return new ResponseEventDTO
            {
                Id = entity.Id,
                OrganizerId = entity.OrganizerId,
                Title = entity.Title,
                Description = entity.Description,
                Category = entity.Category,
                Status = entity.Status,
                Venue = new ResponseVenueDTO
                {
                    Name = entity.Venue.Name,
                    Address = entity.Venue.Address,
                    City = entity.Venue.City,
                    State = entity.Venue.State,
                    Zip = entity.Venue.Zip,
                },
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
    }
}
