using BoardGamesCenter.Entities;

namespace BoardGamesCenter.Services.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAdminUser();
    }
}
