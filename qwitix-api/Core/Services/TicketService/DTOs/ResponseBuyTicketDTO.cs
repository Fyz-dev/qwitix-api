using qwitix_api.Core.Services.DTOs;

namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public record ResponseBuyTicketDTO
    {
        public required string url { get; init; }
    }
}
