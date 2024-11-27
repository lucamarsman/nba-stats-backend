using Microsoft.EntityFrameworkCore;
using nba_stats_api.Models;

    public class TeamRepository : ITeamRepository
    {
        private readonly NBAStatsDbContext _dbContext;

        public TeamRepository(NBAStatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _dbContext.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        public async Task AddTeamAsync(Team team)
        {
            team.UpdatedAt = DateTime.UtcNow;
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            var existingTeam = await _dbContext.Teams.FindAsync(team.TeamId);
            if (existingTeam == null)
            {
                return false;
            }


            // Update existing team
            existingTeam.TeamId = team.TeamId;
            existingTeam.City = team.City;
            existingTeam.FullName = team.FullName;
            existingTeam.NickName = team.NickName;
            existingTeam.State = team.State;
            existingTeam.YearFounded = team.YearFounded;
            existingTeam.UpdatedAt = DateTime.UtcNow;

            _dbContext.Teams.Update(existingTeam);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpsertTeamAsync(Team team)
        {
            var existingTeam = await _dbContext.Teams.FindAsync(team.TeamId);

            if (existingTeam == null)
            {
                // Add new team
                team.UpdatedAt = DateTime.UtcNow;
                await _dbContext.Teams.AddAsync(team);
            }
            else
            {
                // Update existing team
                existingTeam.TeamId = team.TeamId;
                existingTeam.City = team.City;
                existingTeam.FullName = team.FullName;
                existingTeam.NickName = team.NickName;
                existingTeam.State = team.State;
                existingTeam.YearFounded = team.YearFounded;
                existingTeam.UpdatedAt = DateTime.UtcNow;

                _dbContext.Teams.Update(existingTeam);
            }

            await _dbContext.SaveChangesAsync();
        }

    public async Task<int> GetTeamIdByAbbreviation(string abbreviation)
    {
        return await _dbContext.Teams
        .Where(t => t.Abbreviation == abbreviation)
        .Select(t => t.TeamId)
        .FirstOrDefaultAsync();
    }
}

