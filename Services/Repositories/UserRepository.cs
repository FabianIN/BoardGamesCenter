using BoardGamesCenter.Contexts;
using BoardGamesCenter.Entities;

namespace BoardGamesCenter.Services.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly GamesContext _context;

        public UserRepository(GamesContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<User> GetAdminUser()
        {
            return _context.Users
                .Where(u => u.IsAdmin && (u.Deleted == false || u.Deleted == null))
                .ToList();
        }
    }
}
