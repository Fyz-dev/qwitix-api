using qwitix_api.Core.Entities;

namespace qwitix_api.Core.Repositories
{
    public interface IUserRepository
    {
        Task Create(IUser user);

        Task<IUser> GetById(string id);

        Task UpdateById(string id, IUser user);
    }
}
