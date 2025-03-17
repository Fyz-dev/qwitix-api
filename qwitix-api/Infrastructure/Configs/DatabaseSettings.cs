namespace qwitix_api.Infrastructure.Configs
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string OrganizersCollectionName { get; set; } = null!;

        public string EventsCollectionName { get; set; } = null!;

        public string TicketsCollectionName { get; set; } = null!;

        public string TransactionsCollectionName { get; set; } = null!;
    }
}
