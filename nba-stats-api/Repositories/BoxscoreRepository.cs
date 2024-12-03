
using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

public class BoxscoreRepository : IBoxscoreRepository
{
    private readonly NBAStatsDbContext _dbContext;

    public BoxscoreRepository(NBAStatsDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task AddBoxscoreAsync(Boxscore boxscore)
    {
        boxscore.UpdatedAt = DateTime.UtcNow;
        await _dbContext.Boxscores.AddAsync(boxscore);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Boxscore>> GetAllBoxscoresAsync()
    {
        return await _dbContext.Boxscores.ToListAsync();
    }

    public async Task<Boxscore> GetBoxscoreByIdAsync(int gameId)
    {
        return await _dbContext.Boxscores.FirstOrDefaultAsync(bs => bs.gameId == gameId);
    }

    public async Task<bool> UpdateBoxscoreAsync(Boxscore boxscore)
    {
        var existingBoxscore = await _dbContext.Boxscores.FindAsync(boxscore.gameId);
        if (existingBoxscore == null)
        {
            return false;
        }

        // Update existing game
        existingBoxscore.gameId = boxscore.gameId;
        existingBoxscore.playerId = boxscore.playerId;
        existingBoxscore.playerName = boxscore.playerName;
        existingBoxscore.gameComment = boxscore.gameComment;
        existingBoxscore.position = boxscore.position;
        existingBoxscore.minutes = boxscore.minutes;
        existingBoxscore.assists = boxscore.assists;
        existingBoxscore.rebounds = boxscore.rebounds;
        existingBoxscore.blocks = boxscore.blocks;
        existingBoxscore.turnovers = boxscore.turnovers;
        existingBoxscore.points = boxscore.points;
        existingBoxscore.steals = boxscore.steals;
        existingBoxscore.plusMinus = boxscore.plusMinus;
        existingBoxscore.fieldGoalsAttempted = boxscore.fieldGoalsAttempted;
        existingBoxscore.fieldGoalsMade = boxscore.fieldGoalsMade;
        existingBoxscore.fieldGoalPercentage = boxscore.fieldGoalPercentage;
        existingBoxscore.threePointersAttempted = boxscore.threePointersAttempted;
        existingBoxscore.threePointersMade = boxscore.threePointersMade;
        existingBoxscore.threePointerPercentage = boxscore.threePointerPercentage;

        existingBoxscore.UpdatedAt = DateTime.UtcNow;

        _dbContext.Boxscores.Update(existingBoxscore);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task UpsertBoxscoreAsync(Boxscore boxscore)
    {
        var existingBoxscore = await _dbContext.Boxscores.FindAsync(boxscore.gameId);

        if (existingBoxscore == null)
        {
            // Add new game
            boxscore.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Boxscores.AddAsync(boxscore);
        }
        else
        {
            _dbContext.Boxscores.Update(existingBoxscore);
        }

        await _dbContext.SaveChangesAsync();
    }
}
