using BoardGamesCenter.Contexts;
using BoardGamesCenter.Entities;

namespace BoardGamesCenter.Services.Repositories
{
    public class AuthorRepository : Repository <Author>, IAuthorRepository
    {
        private readonly GamesContext _context;

        public AuthorRepository(GamesContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
