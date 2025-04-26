using System.ComponentModel.DataAnnotations;

namespace qwitix_api.Core.Services.DTOs
{
    public record UrlResponseDTO
    {
        [Required]
        public required string Url { get; init; }
    }
}
