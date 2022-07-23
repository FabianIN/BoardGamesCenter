using BoardGamesCenter.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesCenter.Contexts
{
    public class GamesContext : DbContext
    {
        public GamesContext(DbContextOptions<GamesContext> options)
            :base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
     }
}
