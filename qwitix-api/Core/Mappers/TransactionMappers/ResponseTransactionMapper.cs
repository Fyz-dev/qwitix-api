using qwitix_api.Core.Models;
using qwitix_api.Core.Services.TicketService.DTOs;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Core.Mappers.TransactionMappers
{
    public class ResponseTransactionMapper(
        IMapper<TicketPurchaseDTO, TicketPurchase> ticketPurchaseMapper
    ) : BaseMapper<ResponseTransactionDTO, Transaction>
    {
        private readonly IMapper<TicketPurchaseDTO, TicketPurchase> _ticketPurchaseMapper =
            ticketPurchaseMapper;

        public override Transaction ToEntity(ResponseTransactionDTO dto)
        {
            throw new NotImplementedException();
        }

        public override ResponseTransactionDTO ToDto(Transaction entity)
        {
            return new ResponseTransactionDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Tickets = _ticketPurchaseMapper.ToDtoList(entity.Tickets),
                Currency = entity.Currency,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
    }
}
