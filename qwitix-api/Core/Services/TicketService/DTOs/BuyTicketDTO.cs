﻿namespace qwitix_api.Core.Services.TicketService.DTOs
{
    public record BuyTicketDTO
    {
        public required string SuccessUrl { get; init; }

        public required string CancelUrl { get; init; }

        public required List<TicketPurchaseDTO> Tickets { get; init; }
    }
}
