using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageRepository(IOptions<AzureBlobStorage> azureBlobStorage)
        {
            _containerClient = new BlobContainerClient(
                azureBlobStorage.Value.ConnectionString,
                azureBlobStorage.Value.ContainerName
            );
        }

        public async Task<string> UploadFileAsync(IFormFile file, string eventId)
        {
            await foreach (var blob in _containerClient.GetBlobsAsync(prefix: eventId))
                await _containerClient.DeleteBlobIfExistsAsync(blob.Name);

            var extension = Path.GetExtension(file.FileName);
            var blobName = $"{eventId}{extension}";

            var blobClient = _containerClient.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobName;
        }

        public async Task<(Stream stream, string contentType)?> GetFileAsync(string blobPath)
        {
            var blobClient = _containerClient.GetBlobClient(blobPath);

            if (!await blobClient.ExistsAsync())
                return null;

            var response = await blobClient.DownloadAsync();

            return (
                response.Value.Content,
                response.Value.Details.ContentType ?? "application/octet-stream"
            );
        }

        public async Task DeleteFileAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
