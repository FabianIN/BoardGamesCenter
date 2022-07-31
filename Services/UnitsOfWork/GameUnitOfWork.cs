using BoardGamesCenter.Contexts;
using BoardGamesCenter.Services.Repositories;

namespace BoardGamesCenter.Services.UnitsOfWork
{
    public class GameUnitOfWork : IGameUnitOfWork
    {
        private readonly GamesContext _context;

        public GameUnitOfWork(GamesContext context, IGameRepository games, IAuthorRepository authors, IPublisherRepository publishers)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Games = games ?? throw new ArgumentNullException(nameof(context));
            Authors = authors ?? throw new ArgumentNullException(nameof(context));
            Publishers = publishers ?? throw new ArgumentNullException(nameof(context));

        }
        public IGameRepository Games { get; }

        public IAuthorRepository Authors { get; }

        public IPublisherRepository Publishers { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
