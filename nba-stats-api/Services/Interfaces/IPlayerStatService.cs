
public interface IPlayerStatService
{
    Task<PlayerStat> GetStatByIdAsync(int playerId);         // Get a players stat by id
    Task<List<PlayerStat>> GetPlayerStatsAsync();               // Get all player stats
    Task<bool> UpdatePlayerStatAsync(PlayerStat stat);           // Update a players stats
    Task AddStatAsync(PlayerStat stat);                    // Add a new game
    Task<List<PlayerStat>> SeedStats(string permode);              // Seed PlayerStat table
}

