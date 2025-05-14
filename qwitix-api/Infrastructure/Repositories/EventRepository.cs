using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class EventRepository : MongoRepository<Event>, IEventRepository
    {
        public EventRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.EventsCollectionName) { }

        public async Task Create(Event eventModel)
        {
            await _collection.InsertOneAsync(eventModel);
        }

        public async Task<(IEnumerable<Event> Items, int TotalCount)> GetAll(
            string? organizerId,
            int offset,
            int limit,
            EventStatus? status = null,
            string? searchQuery = null,
            List<string>? categories = null
        )
        {
            var filters = new List<FilterDefinition<Event>>
            {
                Builders<Event>.Filter.Eq(e => e.IsDeleted, false),
            };

            if (!string.IsNullOrWhiteSpace(organizerId))
                filters.Add(Builders<Event>.Filter.Eq(e => e.OrganizerId, organizerId));

            if (status.HasValue)
            {
                switch (status.Value)
                {
                    case EventStatus.Live:
                        filters.Add(
                            Builders<Event>.Filter.Eq(e => e.Status, EventStatus.Scheduled)
                        );
                        filters.Add(Builders<Event>.Filter.Lte(e => e.StartDate, DateTime.UtcNow));
                        filters.Add(Builders<Event>.Filter.Gte(e => e.EndDate, DateTime.UtcNow));
                        break;

                    case EventStatus.Ended:
                        filters.Add(
                            Builders<Event>.Filter.Eq(e => e.Status, EventStatus.Scheduled)
                        );
                        filters.Add(Builders<Event>.Filter.Lte(e => e.EndDate, DateTime.UtcNow));
                        break;

                    default:
                        filters.Add(Builders<Event>.Filter.Eq(e => e.Status, status.Value));
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
                filters.Add(
                    Builders<Event>.Filter.Regex(
                        e => e.Title,
                        new BsonRegularExpression(searchQuery, "i")
                    )
                );

            if (categories is { Count: > 0 })
            {
                var categoryFilters = categories
                    .Select(category =>
                        Builders<Event>.Filter.Regex(
                            e => e.Category,
                            new BsonRegularExpression(category, "i")
                        )
                    )
                    .ToList();

                filters.Add(Builders<Event>.Filter.Or(categoryFilters));
            }

            var filter = Builders<Event>.Filter.And(filters);

            var totalCount = await _collection.CountDocumentsAsync(filter);

            var items = await _collection
                .Find(filter)
                .SortByDescending(e => e.CreatedAt)
                .Skip(offset)
                .Limit(limit)
                .ToListAsync();

            return (items, (int)totalCount);
        }

        public async Task<Event?> GetById(string id)
        {
            var filter = Builders<Event>.Filter.And(
                Builders<Event>.Filter.Eq(e => e.Id, id),
                Builders<Event>.Filter.Eq(e => e.IsDeleted, false)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetById(params string[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Enumerable.Empty<Event>();

            var filter = Builders<Event>.Filter.And(
                Builders<Event>.Filter.In(e => e.Id, ids),
                Builders<Event>.Filter.Eq(e => e.IsDeleted, false)
            );

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateById(string id, Event eventModel)
        {
            eventModel.UpdatedAt = DateTime.UtcNow;

            var filter = Builders<Event>.Filter.Eq(e => e.Id, id);

            var result = await _collection.ReplaceOneAsync(filter, eventModel);

            if (result.ModifiedCount == 0)
                throw new Exception("Event not found or no changes made.");
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
            var update = Builders<Event>.Update.Set(e => e.IsDeleted, true);

            var result = await _collection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
                throw new Exception("Event not found or already deleted.");
        }

        public async Task<IEnumerable<string>> GetUniqueCategories()
        {
            var categories = await _collection
                .Aggregate()
                .Match(e => !e.IsDeleted)
                .AppendStage<BsonDocument>(
                    new BsonDocument
                    {
                        {
                            "$group",
                            new BsonDocument
                            {
                                { "_id", new BsonDocument("$toLower", "$category") },
                            }
                        },
                    }
                )
                .AppendStage<BsonDocument>(
                    new BsonDocument { { "$sort", new BsonDocument("_id", 1) } }
                )
                .Project<BsonDocument>(new BsonDocument { { "Category", "$_id" }, { "_id", 0 } })
                .ToListAsync();

            return categories.Select(c => c["Category"].AsString);
        }
    }
}
