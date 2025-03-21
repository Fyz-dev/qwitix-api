using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);

        Task<User> GetById(string id);

        Task<User?> GetUserByRefreshToken(string refreshToken);

        Task<User?> GetUserByEmail(string email);

        Task UpdateById(string id, User user);
    }
}
