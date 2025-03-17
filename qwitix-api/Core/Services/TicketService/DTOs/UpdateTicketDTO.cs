namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public class UpdateTicketDTO
    {
        public string? Name { get; init; }

        public string? Details { get; init; }

        public decimal? Price { get; init; }

        public int? Quantity { get; init; }
    }
}
