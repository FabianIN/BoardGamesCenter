using BoardGamesCenter.Contexts;
using BoardGamesCenter.Services.Repositories;
using BoardGamesCenter.Services.UnitsOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "https://localhost:7049",
                    ValidAudience = "https://localhost:7049",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("parolamea1234"))
                };
            });

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }

    }
}
