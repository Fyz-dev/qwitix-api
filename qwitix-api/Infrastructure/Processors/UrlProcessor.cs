using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using qwitix_api.Core.Processors;

namespace qwitix_api.Infrastructure.Processors
{
    public class UrlProcessor(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        : IUrlProcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly LinkGenerator _linkGenerator = linkGenerator;

        public string GetMediaUrl(string blobName)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                throw new InvalidOperationException("No HttpContext available.");

            return _linkGenerator.GetUriByAction(
                    httpContext,
                    action: "Get",
                    controller: "Media",
                    values: new { blobName = blobName }
                ) ?? throw new Exception("Could not generate media URL.");
        }
    }
}
