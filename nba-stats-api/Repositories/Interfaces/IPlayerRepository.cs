using nba_stats_api.Models;

public interface IPlayerRepository
{
    Task<Player> GetPlayerByIdAsync(int playerId);         // Get a player by ID
    Task<List<Player>> GetAllPlayersAsync();               // Get all players
    Task<bool> UpdatePlayerAsync(Player player);           // Update a player
    Task AddPlayerAsync(Player player);                    // Add a new player
    Task UpsertPlayerAsync(Player player);                 // Add or update a player
}

