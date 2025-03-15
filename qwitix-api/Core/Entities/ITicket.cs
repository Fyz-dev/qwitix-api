namespace qwitix_api.Core.Entities
{
    public interface ITicket : IBaseEntity
    {
        public string EventId { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Sold { get; set; }
    }
}
