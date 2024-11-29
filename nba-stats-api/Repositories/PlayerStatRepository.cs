

using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

public class PlayerStatRepository : IPlayerStatRepository
{
    private readonly NBAStatsDbContext _dbContext;

    public PlayerStatRepository(NBAStatsDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task AddPlayerStatAsync(PlayerStat stat)
    {
        stat.UpdatedAt = DateTime.UtcNow;
        await _dbContext.PlayerStats.AddAsync(stat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<PlayerStat>> GetPlayerStatAsync()
    {
        return await _dbContext.PlayerStats.ToListAsync();
    }

    public async Task<PlayerStat> GetStatByIdAsync(int playerId)
    {
        return await _dbContext.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerId == playerId);
    }

    public async Task<bool> UpdatePlayerStatAsync(PlayerStat stat)
    {
        var existingStat = await _dbContext.PlayerStats.FindAsync(stat.PlayerId);
        if (existingStat == null)
        {
            return false;
        }

        // Update existing player stat
        existingStat.TeamId = stat.TeamId;
        existingStat.SeasonType = stat.SeasonType;
        existingStat.GamesPlayed = stat.GamesPlayed;
        existingStat.Points = stat.Points;
        existingStat.Assists = stat.Assists;
        existingStat.Rebounds = stat.Rebounds;
        existingStat.DefensiveRebounds = stat.DefensiveRebounds;
        existingStat.OffensiveRebounds = stat.OffensiveRebounds;
        existingStat.FreeThrowsAttempted = stat.FreeThrowsAttempted;
        existingStat.FreeThrowsMade = stat.FreeThrowsMade;
        existingStat.FreeThrowPercentage = stat.FreeThrowPercentage;
        existingStat.FieldGoalsAttempted = stat.FieldGoalsAttempted;
        existingStat.FieldGoalsMade = stat.FieldGoalsMade;
        existingStat.FieldGoalPercentage = stat.FieldGoalPercentage;
        existingStat.ThreePointersAttempted = stat.ThreePointersAttempted;
        existingStat.ThreePointersMade = stat.ThreePointersMade;
        existingStat.ThreePointerPercentage = stat.ThreePointersMade;
        existingStat.Steals = stat.Steals;
        existingStat.Blocks = stat.Blocks;
        existingStat.Turnovers = stat.Turnovers;
        existingStat.Minutes = stat.Minutes;
        existingStat.PlusMinus = stat.PlusMinus;
            
        existingStat.UpdatedAt = DateTime.UtcNow;

        _dbContext.PlayerStats.Update(existingStat);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task UpsertPlayerStatAsync(PlayerStat stat)
    {
        var existingStat = await _dbContext.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerId == stat.PlayerId && ps.PerMode == stat.PerMode && ps.Season == stat.Season);

        if (existingStat == null)
        {
            // Add new stat
            stat.UpdatedAt = DateTime.UtcNow;
            await _dbContext.PlayerStats.AddAsync(stat);
        }
        else
        {
            // Update existing player stat
            existingStat.TeamId = stat.TeamId;
            existingStat.SeasonType = stat.SeasonType;
            existingStat.GamesPlayed = stat.GamesPlayed;
            existingStat.Points = stat.Points;
            existingStat.Assists = stat.Assists;
            existingStat.Rebounds = stat.Rebounds;
            existingStat.DefensiveRebounds = stat.DefensiveRebounds;
            existingStat.OffensiveRebounds = stat.OffensiveRebounds;
            existingStat.FreeThrowsAttempted = stat.FreeThrowsAttempted;
            existingStat.FreeThrowsMade = stat.FreeThrowsMade;
            existingStat.FreeThrowPercentage = stat.FreeThrowPercentage;
            existingStat.FieldGoalsAttempted = stat.FieldGoalsAttempted;
            existingStat.FieldGoalsMade = stat.FieldGoalsMade;
            existingStat.FieldGoalPercentage = stat.FieldGoalPercentage;
            existingStat.ThreePointersAttempted = stat.ThreePointersAttempted;
            existingStat.ThreePointersMade = stat.ThreePointersMade;
            existingStat.ThreePointerPercentage = stat.ThreePointersMade;
            existingStat.Steals = stat.Steals;
            existingStat.Blocks = stat.Blocks;
            existingStat.Turnovers = stat.Turnovers;
            existingStat.Minutes = stat.Minutes;
            existingStat.PlusMinus = stat.PlusMinus;

            existingStat.UpdatedAt = DateTime.UtcNow;

            _dbContext.PlayerStats.Update(existingStat);
        }

        await _dbContext.SaveChangesAsync();
    }
}

