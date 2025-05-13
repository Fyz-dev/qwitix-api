namespace qwitix_api.Infrastructure.Configs
{
    public class AzureBlobStorage
    {
        public string ConnectionString { get; set; } = null!;
        public string ContainerName { get; set; } = null!;
    }
}
