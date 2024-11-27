using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

public class PlayerRepository : IPlayerRepository
{
    private readonly NBAStatsDbContext _dbContext;

    public PlayerRepository(NBAStatsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// Get a player by ID.
    public async Task<Player> GetPlayerByIdAsync(int playerId)
    {
        return await _dbContext.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);
    }

    /// Get all players.
    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _dbContext.Players.ToListAsync();
    }

    /// Update an existing player.
    public async Task<bool> UpdatePlayerAsync(Player player)
    {
        var existingPlayer = await _dbContext.Players.FindAsync(player.PlayerId);
        if (existingPlayer == null)
            return false;

        existingPlayer.TeamId = player.TeamId;
        existingPlayer.TeamName = player.TeamName;
        existingPlayer.TeamCity = player.TeamCity;
        existingPlayer.TeamAbbreviation = player.TeamAbbreviation;
        existingPlayer.FirstName = player.FirstName;
        existingPlayer.LastName = player.LastName;
        existingPlayer.DisplayName = player.DisplayName;
        existingPlayer.Height = player.Height;
        existingPlayer.Weight = player.Weight;
        existingPlayer.Position = player.Position;
        existingPlayer.SeasonExperience = player.SeasonExperience;
        existingPlayer.JerseyNumber = player.JerseyNumber;
        existingPlayer.BirthDate = player.BirthDate;
        existingPlayer.Country = player.Country;
        existingPlayer.LastAffiliation = player.LastAffiliation;
        existingPlayer.DraftYear = player.DraftYear;
        existingPlayer.DraftRound = player.DraftRound;
        existingPlayer.DraftNumber = player.DraftNumber;
        existingPlayer.PointsPerGame = player.PointsPerGame;
        existingPlayer.ReboundsPerGame = player.ReboundsPerGame;
        existingPlayer.AssistsPerGame = player.AssistsPerGame;
        existingPlayer.PlayerImpactEstimate = player.PlayerImpactEstimate;
        existingPlayer.IsActive = player.IsActive;
        existingPlayer.IsNBAPlayer = player.IsNBAPlayer;
        existingPlayer.IsGreatest75 = player.IsGreatest75;
        existingPlayer.UpdatedAt = DateTime.UtcNow;

        _dbContext.Players.Update(existingPlayer);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// Add a new player to the database.
    public async Task AddPlayerAsync(Player player)
    {
        player.UpdatedAt = DateTime.UtcNow; // Ensure UpdatedAt is set
        await _dbContext.Players.AddAsync(player);
        await _dbContext.SaveChangesAsync();
    }

    /// Insert or update a player based on whether they already exist.
    public async Task UpsertPlayerAsync(Player player)
    {
        var existingPlayer = await _dbContext.Players.FindAsync(player.PlayerId);

        if (existingPlayer == null)
        {
            // Add new player
            player.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Players.AddAsync(player);
        }
        else
        {
            // Update existing player
            existingPlayer.TeamId = player.TeamId;
            existingPlayer.TeamName = player.TeamName;
            existingPlayer.TeamCity = player.TeamCity;
            existingPlayer.TeamAbbreviation = player.TeamAbbreviation;
            existingPlayer.FirstName = player.FirstName;
            existingPlayer.LastName = player.LastName;
            existingPlayer.DisplayName = player.DisplayName;
            existingPlayer.Height = player.Height;
            existingPlayer.Weight = player.Weight;
            existingPlayer.Position = player.Position;
            existingPlayer.SeasonExperience = player.SeasonExperience;
            existingPlayer.JerseyNumber = player.JerseyNumber;
            existingPlayer.BirthDate = player.BirthDate;
            existingPlayer.Country = player.Country;
            existingPlayer.LastAffiliation = player.LastAffiliation;
            existingPlayer.DraftYear = player.DraftYear;
            existingPlayer.DraftRound = player.DraftRound;
            existingPlayer.DraftNumber = player.DraftNumber;
            existingPlayer.PointsPerGame = player.PointsPerGame;
            existingPlayer.ReboundsPerGame = player.ReboundsPerGame;
            existingPlayer.AssistsPerGame = player.AssistsPerGame;
            existingPlayer.PlayerImpactEstimate = player.PlayerImpactEstimate;
            existingPlayer.IsActive = player.IsActive;
            existingPlayer.IsNBAPlayer = player.IsNBAPlayer;
            existingPlayer.IsGreatest75 = player.IsGreatest75;
            existingPlayer.UpdatedAt = DateTime.UtcNow;

            _dbContext.Players.Update(existingPlayer);
        }

        await _dbContext.SaveChangesAsync();
    }
}
