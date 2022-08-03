using BoardGamesCenter.Contexts;
using BoardGamesCenter.Services.Repositories;
using BoardGamesCenter.Services.UnitsOfWork;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesCenter
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["ConnectionStrings:GamesDbConnectionString"];
            builder.Services.AddDbContext<GamesContext>(o => o.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

            builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
            builder.Services.AddScoped<IGameUnitOfWork, GameUnitOfWork>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void Configure(WebApplication app)//, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

    }
}
