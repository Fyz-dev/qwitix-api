using qwitix_api.Core.Models;
using qwitix_api.Core.Services.TicketService.DTOs;

namespace qwitix_api.Core.Mappers.TicketMappers
{
    public class TicketPurchaseMapper : BaseMapper<TicketPurchaseDTO, TicketPurchase>
    {
        public override TicketPurchase ToEntity(TicketPurchaseDTO dto)
        {
            return new TicketPurchase { TicketId = dto.TicketId, Quantity = dto.Quantity };
        }

        public override TicketPurchaseDTO ToDto(TicketPurchase entity)
        {
            return new TicketPurchaseDTO { TicketId = entity.TicketId, Quantity = entity.Quantity };
        }
    }
}
