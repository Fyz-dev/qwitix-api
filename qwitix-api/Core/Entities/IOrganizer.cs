namespace qwitix_api.Core.Entities
{
    public interface IOrganizer : IBaseEntity
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public string ImageUrl { get; set; }

        public bool IsVerified { get; set; }
    }
}
