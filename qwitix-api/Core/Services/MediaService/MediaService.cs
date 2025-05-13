using qwitix_api.Core.Repositories;

namespace qwitix_api.Core.Services.MediaService
{
    public class MediaService
    {
        private readonly IBlobStorageRepository _blobStorageRepository;

        public MediaService(IBlobStorageRepository blobStorageRepository)
        {
            _blobStorageRepository = blobStorageRepository;
        }

        public async Task<(Stream stream, string contentType)?> GetFileAsync(string blobPath)
        {
            var file = await _blobStorageRepository.GetFileAsync(blobPath);

            if (file == null)
                return null;

            return file;
        }
    }
}
