namespace qwitix_api.Core.Services.DTOs
{
    public record ResponseBaseDTO
    {
        public required string Id { get; set; }

        public required DateTime CreatedAt { get; set; }

        public required DateTime UpdatedAt { get; set; }
    }
}
