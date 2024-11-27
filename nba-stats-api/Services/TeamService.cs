using nba_stats_api.API;
using nba_stats_api.Models;
using System.Text.Json;

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly HttpClient _httpClient;

        public TeamService(ITeamRepository teamRepository, HttpClient httpClient)
        {
            _teamRepository = teamRepository;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            // Check the database first
            var existingTeam = await _teamRepository.GetTeamByIdAsync(teamId);
            if (existingTeam != null && existingTeam.UpdatedAt > DateTime.UtcNow.AddDays(-1))
            {
                return existingTeam; // Return the player if the data is up-to-date
            }

            return null;
        }

        public async Task<List<Team>> SeedTeams()
        {
            try
            {
                // Fetch data from the API
                var response = await _httpClient.GetAsync("/teams");
                response.EnsureSuccessStatusCode();

                // Parse the response JSON
                var responseData = await response.Content.ReadAsStringAsync();
                var teamResponses = JsonSerializer.Deserialize<List<TeamResponse>>(responseData);

                if (teamResponses == null || !teamResponses.Any())
                {
                    throw new Exception("No teams were retrieved from the API.");
                }

                // Map and upsert teams
                foreach (var teamResponse in teamResponses)
                {
                    var team = new Team
                    {
                        TeamId = teamResponse.TeamId,
                        Abbreviation = teamResponse.Abbreviation,
                        City = teamResponse.City,
                        FullName = teamResponse.FullName,
                        NickName = teamResponse.NickName,
                        State = teamResponse.State,
                        YearFounded = teamResponse.YearFounded,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _teamRepository.UpsertTeamAsync(team);
                }

                // Return all teams from the database
                return await _teamRepository.GetAllTeamsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while seeding teams: {ex.Message}");
                throw;
            }
        }


        public async Task AddTeamAsync(Team team)
        {
            await _teamRepository.AddTeamAsync(team);
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeamsAsync();
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            return await _teamRepository.UpdateTeamAsync(team);
        }
    }
