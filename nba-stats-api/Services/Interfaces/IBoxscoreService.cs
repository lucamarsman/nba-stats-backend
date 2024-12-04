
public interface IBoxscoreService
{
    Task<Boxscore> GetBoxscoreByIdAsync(int gameId);         // Get a single boxscore by ID
    Task<List<Boxscore>> GetAllBoxscoresAsync();               // Get all boxscores
    Task<bool> UpdateBoxscoreAsync(Game game);           // Update an existing boxscore
    Task AddBoxscoreAsync(Boxscore boxscore);                    // Add a new boxscore
    Task<List<Boxscore>> SeedBoxscores();              // Seed boxscore table
}

