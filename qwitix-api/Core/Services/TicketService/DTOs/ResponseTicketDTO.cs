using qwitix_api.Core.Services.DTOs;

namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public record ResponseTicketDTO : ResponseBaseDTO
    {
        public required string EventId { get; set; }

        public required string Name { get; set; }

        public string? Details { get; set; }

        public required decimal Price { get; set; }

        public required int Quantity { get; set; }
    }
}
