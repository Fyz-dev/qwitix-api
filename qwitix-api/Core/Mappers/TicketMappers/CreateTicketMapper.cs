using qwitix_api.Core.Models;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Core.Mappers.TicketMappers
{
    public class CreateTicketMapper : BaseMapper<CreateTicketDTO, Ticket>
    {
        public override Ticket ToEntity(CreateTicketDTO dto)
        {
            return new Ticket
            {
                EventId = dto.EventId,
                Name = dto.Name,
                Details = dto.Details,
                Price = dto.Price,
                Quantity = dto.Quantity,
            };
        }

        public override CreateTicketDTO ToDto(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
