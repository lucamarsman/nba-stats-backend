

using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

public class TeamStatRepository : ITeamStatRepository
{
    private readonly NBAStatsDbContext _dbContext;

    public TeamStatRepository(NBAStatsDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task AddTeamStatAsync(TeamStat stat)
    {
        stat.UpdatedAt = DateTime.UtcNow;
        await _dbContext.TeamStats.AddAsync(stat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TeamStat> GetStatByIdAsync(int teamId)
    {
        return await _dbContext.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == teamId);
    }

    public async Task<List<TeamStat>> GetTeamStatAsync()
    {
        return await _dbContext.TeamStats.ToListAsync();
    }

    public async Task<bool> UpdateTeamStatAsync(TeamStat stat)
    {
        var existingStat = await _dbContext.TeamStats.FindAsync(stat.TeamId);
        if (existingStat == null)
        {
            return false;
        }

        // Update existing team stat
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

        _dbContext.TeamStats.Update(existingStat);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task UpsertTeamStatAsync(TeamStat stat)
    {
        var existingStat = await _dbContext.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == stat.TeamId && ts.PerMode == stat.PerMode && ts.Season == stat.Season);

        if (existingStat == null)
        {
            // Add new stat
            stat.UpdatedAt = DateTime.UtcNow;
            Console.WriteLine($"Inserting stat for TeamId: {stat.TeamId}, PerMode: {stat.PerMode}, Points: {stat.Points}");
            await _dbContext.TeamStats.AddAsync(stat);
        }
        else
        {
            // Update existing team stat
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

            Console.WriteLine($"Updating stat for TeamId: {stat.TeamId}, PerMode: {stat.PerMode}, Points: {stat.Points}");

            _dbContext.TeamStats.Update(existingStat);
        }

        await _dbContext.SaveChangesAsync();
    }
}

