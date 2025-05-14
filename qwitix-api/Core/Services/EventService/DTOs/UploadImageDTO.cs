namespace qwitix_api.Core.Services.EventService.DTOs
{
    public record UploadImageDTO
    {
        public IFormFile Image { get; set; } = null!;
    }
}
