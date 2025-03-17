using Microsoft.Extensions.Options;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Infrastructure.Configs;

namespace qwitix_api.Infrastructure.Repositories
{
    public class TicketRepository : MongoRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(IOptions<DatabaseSettings> databaseSettings)
            : base(databaseSettings, databaseSettings.Value.TicketsCollectionName) { }

        public Task BuyById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticket>> GetAll(string eventId, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task RefundById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(string id, Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
