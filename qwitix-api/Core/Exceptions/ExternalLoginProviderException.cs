namespace qwitix_api.Core.Exceptions
{
    public class ExternalLoginProviderException(string provider, string message)
        : Exception($"External login provider: {provider} error occurred: {message}");
}
