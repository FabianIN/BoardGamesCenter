using BoardGamesCenter.Entities;

namespace BoardGamesCenter.Services.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetGameDetails(Guid gameId);
    }
}
