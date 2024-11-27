using nba_stats_api.Models;



    public interface ITeamService
    {
        Task<Team> GetTeamByIdAsync(int teamId);         // Get a single team by id
        Task<List<Team>> GetAllTeamsAsync();               // Get all teams
        Task<bool> UpdateTeamAsync(Team team);           // Update an existing team
        Task AddTeamAsync(Team team);                    // Add a new team
        Task<List<Team>> SeedTeams();              // Seed team table
    }

