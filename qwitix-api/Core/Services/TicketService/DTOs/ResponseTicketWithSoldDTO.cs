namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public record ResponseTicketWithSoldDTO : ResponseTicketDTO
    {
        public int Sold { get; set; }
    }
}
