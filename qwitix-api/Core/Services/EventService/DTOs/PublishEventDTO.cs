namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record PublishEventDTO
    {
        public required DateTime StartDate { get; init; }

        public required DateTime EndDate { get; init; }
    }
}
