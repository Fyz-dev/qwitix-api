using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);

        Task<User> GetById(string id);

        Task UpdateById(string id, User user);
    }
}
