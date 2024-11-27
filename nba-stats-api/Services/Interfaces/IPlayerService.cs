using nba_stats_api.Models;

public interface IPlayerService
{
    Task<Player> GetPlayerByIdAsync(int playerId);         // Get a single player by ID
    Task<List<Player>> GetAllPlayersAsync();               // Get all players
    Task<bool> UpdatePlayerAsync(Player player);           // Update an existing player
    Task AddPlayerAsync(Player player);                    // Add a new player
    Task<List<Player>> SeedPlayers();              // Seed player table via team names
}

