using qwitix_api.Core.Enums;

namespace qwitix_api.Core.Entities
{
    public interface IUser : IBaseEntity
    {
        public string GoogleId { get; set; }

        public string StripeCustomerId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}
