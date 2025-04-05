using qwitix_api.Core.Models;
using qwitix_api.Core.Services.TransactionService.DTOs;

namespace qwitix_api.Core.Mappers.TransactionMappers
{
    public class ResponseTransactionMapper : BaseMapper<ResponseTransactionDTO, Transaction>
    {
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
                TicketId = entity.TicketId,
                Amount = entity.Amount,
                Currency = entity.Currency,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
    }
}
