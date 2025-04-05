using qwitix_api.Core.Models;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Core.Mappers.TicketMappers
{
    public class ResponseTicketMapper : BaseMapper<ResponseTicketDTO, Ticket>
    {
        public override Ticket ToEntity(ResponseTicketDTO dto)
        {
            throw new NotImplementedException();
        }

        public override ResponseTicketDTO ToDto(Ticket entity)
        {
            return new ResponseTicketDTO
            {
                Id = entity.Id,
                EventId = entity.EventId,
                Name = entity.Name,
                Details = entity.Details,
                Price = entity.Price,
                Quantity = entity.Quantity,
                Sold = entity.Sold,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
    }
}
