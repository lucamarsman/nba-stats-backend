
public interface IGameService
{
    Task<Game> GetGameByIdAsync(int gameId);         // Get a single game by ID
    Task<List<Game>> GetAllGamesAsync();               // Get all games
    Task<bool> UpdateGameAsync(Game game);           // Update an existing game
    Task AddGameAsync(Game game);                    // Add a new game
    Task<List<Game>> SeedGames();              // Seed game table
    Task<List<Game>> GetUpcomingGamesAsync(DateTime start, DateTime end); // Fetch upcoming games
}

