using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.MediaService;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("media")]
    public class MediaController(MediaService mediaService) : ControllerBase
    {
        private readonly MediaService _mediaService = mediaService;

        [HttpGet("{**blobName}")]
        public async Task<IActionResult> Get(string blobName)
        {
            var file = await _mediaService.GetFileAsync(blobName);

            if (file == null)
                return NotFound();

            return File(file.Value.stream, file.Value.contentType);
        }
    }
}
