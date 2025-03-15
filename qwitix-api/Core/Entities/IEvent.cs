using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Entities
{
    public interface IEvent<TVenue> : IBaseEntity
        where TVenue : IVenue
    {
        public string OrganizerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public EventStatus Status { get; set; }

        public TVenue Venue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
