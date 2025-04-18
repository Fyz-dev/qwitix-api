namespace qwitix_api.Core.Exceptions
{
    public class ValidationException(string message) : ArgumentException(message);
}
