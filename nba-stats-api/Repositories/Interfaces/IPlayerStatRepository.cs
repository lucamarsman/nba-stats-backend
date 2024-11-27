
public interface IPlayerStatRepository
{
    Task<PlayerStat> GetStatByIdAsync(int playerId);         // Get a player by ID
    Task<List<PlayerStat>> GetPlayerStatAsync();               // Get all players
    Task<bool> UpdatePlayerStatAsync(PlayerStat stat);           // Update a player
    Task AddPlayerStatAsync(PlayerStat stat);                    // Add a new player
    Task UpsertPlayerStatAsync(PlayerStat stat);                 // Add or update a player
}

