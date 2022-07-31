using BoardGamesCenter.Contexts;
using BoardGamesCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesCenter.Services.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly GamesContext _context;

        public GameRepository(GamesContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Game GetGameDetails(Guid gameId)
        {
            return _context.Games
                .Where(b => b.Id == gameId && (b.Deleted == false || b.Deleted == null))
                .Include(b => b.Author)
                .FirstOrDefault();
        }
    }   
}
