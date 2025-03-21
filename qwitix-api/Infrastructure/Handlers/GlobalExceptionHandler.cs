using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using qwitix_api.Core.Exceptions;

namespace qwitix_api.Infrastructure.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var (statusCode, message) = GetExceptionDetails(exception);

            _logger.LogError(exception, exception.Message);

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);

            return true;
        }

        private (HttpStatusCode statusCode, string message) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                RefreshTokenException => (HttpStatusCode.Unauthorized, exception.Message),
                _ => (
                    HttpStatusCode.InternalServerError,
                    $"An unexpected error occurred: {exception.Message}"
                ),
            };
        }
    }
}
