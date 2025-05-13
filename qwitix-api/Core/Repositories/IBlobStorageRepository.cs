namespace qwitix_api.Core.Repositories
{
    public interface IBlobStorageRepository
    {
        Task<string> UploadFileAsync(IFormFile file, string eventId);

        Task<(Stream stream, string contentType)?> GetFileAsync(string blobPath);
    }
}
