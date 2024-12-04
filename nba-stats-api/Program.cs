using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:3000") // Frontend origin
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
builder.Services.AddDbContext<NBAStatsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IPlayerService, PlayerService>(); // Register HttpClient
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddHttpClient<ITeamService, TeamService>(); // Register HttpClient
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddHttpClient<IGameService, GameService>(); // Register HttpClient
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddHttpClient<IPlayerStatService, PlayerStatService>(); // Register HttpClient
builder.Services.AddScoped<IPlayerStatService, PlayerStatService>();
builder.Services.AddScoped<IPlayerStatRepository, PlayerStatRepository>();

builder.Services.AddHttpClient<ITeamStatService, TeamStatService>(); // Register HttpClient
builder.Services.AddScoped<ITeamStatService, TeamStatService>();
builder.Services.AddScoped<ITeamStatRepository, TeamStatRepository>();

builder.Services.AddHttpClient<IBoxscoreService, BoxscoreService>(); // Register HttpClient
builder.Services.AddScoped<IBoxscoreService, BoxscoreService>();
builder.Services.AddScoped<IBoxscoreRepository, BoxscoreRepository>();

builder.Configuration.AddUserSecrets<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
