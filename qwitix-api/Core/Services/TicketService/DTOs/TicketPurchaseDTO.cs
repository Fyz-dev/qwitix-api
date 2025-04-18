namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public record TicketPurchaseDTO
    {
        public required string TicketId { get; init; }

        public required int Quantity { get; init; }
    }
}
