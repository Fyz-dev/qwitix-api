namespace qwitix_api.Infrastructure.Configs
{
    public class CorsOptions
    {
        public string AllowedOriginsRaw = null!;

        public string[] AllowedOrigins =>
            AllowedOriginsRaw.Split(
                ';',
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            ) ?? [];
    }
}
