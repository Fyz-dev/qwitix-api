namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public record CreateTicketDTO
    {
        public required string EventId { get; init; }

        public required string Name { get; init; }

        public string? Details { get; init; }

        public decimal Price { get; init; } = 0;

        public required int Quantity { get; init; }
    }
}
