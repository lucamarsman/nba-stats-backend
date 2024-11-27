
using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

    public class GameRepository : IGameRepository
    {
        private readonly NBAStatsDbContext _dbContext;

        public GameRepository(NBAStatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddGameAsync(Game game)
        {
            game.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Games.AddAsync(game);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _dbContext.Games.ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            return await _dbContext.Games.FirstOrDefaultAsync(g => g.GameId == gameId.ToString());
        }

        public async Task<bool> UpdateGameAsync(Game game)
        {
            var existingGame = await _dbContext.Games.FindAsync(game.GameId);
            if (existingGame == null)
            {
                return false;
            }

            // Update existing game
            existingGame.GameId = game.GameId;
            existingGame.HomeTeamId = game.HomeTeamId;
            existingGame.AwayTeamId = game.AwayTeamId;
            existingGame.GameDate = game.GameDate;
            existingGame.GameStatus = game.GameStatus;
            existingGame.MatchUp = game.MatchUp;
            existingGame.UpdatedAt = DateTime.UtcNow;

            _dbContext.Games.Update(existingGame);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpsertGameAsync(Game game)
        {
            var existingGame = await _dbContext.Games.FindAsync(game.GameId);

            if (existingGame == null)
            {
                // Add new game
                game.UpdatedAt = DateTime.UtcNow;
                await _dbContext.Games.AddAsync(game);
            }
            else
            {
                // Update existing game
                existingGame.GameId = game.GameId;
                existingGame.HomeTeamId = game.HomeTeamId;
                existingGame.AwayTeamId = game.AwayTeamId;
                existingGame.GameDate = game.GameDate;
                existingGame.GameStatus = game.GameStatus;
                existingGame.MatchUp = game.MatchUp;
                existingGame.UpdatedAt = DateTime.UtcNow;

                _dbContext.Games.Update(existingGame);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Game>> GetGamesByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _dbContext.Games
                .Where(game => game.GameDate >= start && game.GameDate <= end)
                .ToListAsync();
        }

}
