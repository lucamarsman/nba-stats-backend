using nba_stats_api.Models;



    public interface ITeamRepository
    {
        Task<Team> GetTeamByIdAsync(int teamId);         // Get a team by ID
        Task<List<Team>> GetAllTeamsAsync();               // Get all teams
        Task<bool> UpdateTeamAsync(Team team);           // Update a team
        Task AddTeamAsync(Team team);                    // Add a new team
        Task UpsertTeamAsync(Team team);                 // Add or update a team
        Task<int> GetTeamIdByAbbreviation(string abbreviation);
    }

