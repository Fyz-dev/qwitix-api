namespace qwitix_api.Core.Entities
{
    public interface IBaseEntity
    {
        string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
