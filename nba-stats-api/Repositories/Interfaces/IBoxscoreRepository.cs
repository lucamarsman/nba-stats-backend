
public interface IBoxscoreRepository
{
    Task<Boxscore> GetBoxscoreByIdAsync(int gameId);         // Get a game by gameId
    Task<List<Boxscore>> GetAllBoxscoresAsync();               // Get all boxscores
    Task<bool> UpdateBoxscoreAsync(Boxscore boxscore);           // Update a boxscore
    Task AddBoxscoreAsync(Boxscore boxscore);                    // Add a new boxscore
    Task UpsertBoxscoreAsync(Boxscore boxscore);                 // Add or update a boxscore
}

