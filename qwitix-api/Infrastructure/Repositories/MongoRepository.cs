using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public abstract class MongoRepository<T>
    {
        protected readonly IMongoCollection<T> _collection;

        protected MongoRepository(
            IOptions<DatabaseSettings> databaseSettings,
            string collectionName
        )
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}
