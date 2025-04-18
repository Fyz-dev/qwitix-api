using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class TransactionRepository : MongoRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.TransactionsCollectionName) { }

        public async Task Create(Transaction transaction)
        {
            await _collection.InsertOneAsync(transaction);
        }

        public async Task<Transaction?> GetByTransactionId(string id)
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.Id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Transaction?> GetByCheckoutSessionId(string id)
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.StripeCheckoutSession, id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.UserId, userId);

            if (status.HasValue)
                filter = Builders<Transaction>.Filter.And(
                    filter,
                    Builders<Transaction>.Filter.Eq(t => t.Status, status.Value)
                );

            return await _collection.Find(filter).Skip(offset).Limit(limit).ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetTotalSoldQuantityForTickets(
            IEnumerable<string> ticketIds
        )
        {
            var ticketIdList = ticketIds.ToList();

            if (!ticketIdList.Any())
                return new Dictionary<string, int>();

            var filter = Builders<Transaction>.Filter.And(
                Builders<Transaction>.Filter.In(
                    t => t.Status,
                    [TransactionStatus.Pending, TransactionStatus.Completed]
                ),
                Builders<Transaction>.Filter.ElemMatch(
                    t => t.Tickets,
                    tp => ticketIdList.Contains(tp.TicketId)
                )
            );

            var transactions = await _collection.Find(filter).ToListAsync();

            return transactions
                .SelectMany(t => t.Tickets)
                .Where(tp => ticketIdList.Contains(tp.TicketId))
                .GroupBy(ticket => ticket.TicketId)
                .ToDictionary(group => group.Key, group => group.Sum(ticket => ticket.Quantity));
        }

        public async Task UpdateById(string id, Transaction transaction)
        {
            transaction.UpdatedAt = DateTime.UtcNow;

            var filter = Builders<Transaction>.Filter.Eq(t => t.Id, id);
            var result = await _collection.ReplaceOneAsync(filter, transaction);

            if (result.ModifiedCount == 0)
                throw new NotFoundException("Transaction not found or no changes made.");
        }
    }
}
