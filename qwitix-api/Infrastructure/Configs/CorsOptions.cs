namespace qwitix_api.Infrastructure.Configs
{
    public class CorsOptions
    {
        private string[] _allowedOrigins = [];

        public string[] AllowedOrigins => _allowedOrigins;

        public string AllowedOriginsRaw
        {
            set =>
                _allowedOrigins =
                    value?.Split(
                        ';',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
                    ) ?? [];
        }
    }
}
