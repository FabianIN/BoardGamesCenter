using BoardGamesCenter.Contexts;
using BoardGamesCenter.Entities;

namespace BoardGamesCenter.Services.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly GamesContext _context;

        public PublisherRepository(GamesContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

    }
}
