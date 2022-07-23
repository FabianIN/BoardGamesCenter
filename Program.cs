using BoardGamesCenter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Startup.ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

Startup.Configure(app);
app.Run();
