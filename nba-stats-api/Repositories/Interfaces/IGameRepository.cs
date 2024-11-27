using nba_stats_api.Models;

public interface IGameRepository
{
    Task<Game> GetGameByIdAsync(int gameId);         // Get a game by ID
    Task<List<Game>> GetAllGamesAsync();               // Get all games
    Task<bool> UpdateGameAsync(Game game);           // Update a game
    Task AddGameAsync(Game game);                    // Add a new game
    Task UpsertGameAsync(Game game);                 // Add or update a game
    Task<List<Game>> GetGamesByDateRangeAsync(DateTime start, DateTime end);
}

