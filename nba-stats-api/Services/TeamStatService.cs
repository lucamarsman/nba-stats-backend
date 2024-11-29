
using Azure;
using Microsoft.VisualBasic;
using System.Text.Json;

public class TeamStatService : ITeamStatService
{
        private readonly ITeamStatRepository _teamStatRepository;
        private readonly HttpClient _httpClient;
        private List<string>? _seasons;

        public TeamStatService(ITeamStatRepository teamStatRepository, HttpClient httpClient)
        {
            _teamStatRepository = teamStatRepository;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }


        public Task AddStatAsync(TeamStat stat)
        {
            throw new NotImplementedException();
        }

        public Task<TeamStat> GetStatByIdAsync(int teamId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TeamStat>> GetTeamStatsAsync()
        {
            var seasonResponse = await _httpClient.GetAsync($"/seasons");
            //seasonResponse.EnsureSuccessStatusCode();
            var responseContent = await seasonResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            return await _teamStatRepository.GetTeamStatAsync();
        }

        public async Task<List<string>> GetSeasonsAsync()
        {
            if (_seasons != null) // Use cached seasons if available
            {
                return _seasons;
            }

            var response = await _httpClient.GetAsync("/seasons");
            //response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            _seasons = JsonSerializer.Deserialize<List<string>>(responseContent);

            return _seasons;
        }

        public async Task<List<TeamStat>> SeedStats(string permode)
        {
            try
            {
                //Fetch NBA seasons from 2010 - current
                var seasons = await GetSeasonsAsync();

                foreach (var season in seasons)
                {
                    await Task.Delay(500);
                    // Fetch data from the API
                    var response = await _httpClient.GetAsync($"/stats/teams?PerMode={permode}&Season={season}");
                    //response.EnsureSuccessStatusCode();

                    var responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Raw API Response for PerMode {permode}: {responseData}");
                    using var document = JsonDocument.Parse(responseData);
                    var statsJson = document.RootElement.GetProperty("LeagueDashTeamStats");

                    // Deserialize the LeagueDashPlayerStats section into a list
                    var leagueDashTeamStats = JsonSerializer.Deserialize<List<TeamStat>>(statsJson.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Map and upsert teams
                    foreach (var statResponse in leagueDashTeamStats)
                    {
                        Console.WriteLine($"Deserialized TeamStat - TeamId: {statResponse.TeamId}, PerMode: {permode}, Points: {statResponse.Points}");

                        var stat = new TeamStat
                        {
                            TeamId = statResponse.TeamId,
                            TeamName = statResponse.TeamName,
                            Season = season,
                            PerMode = permode,
                            GamesPlayed = statResponse.GamesPlayed,
                            Points = statResponse.Points,
                            Assists = statResponse.Assists,
                            Rebounds = statResponse.Rebounds,
                            DefensiveRebounds = statResponse.DefensiveRebounds,
                            OffensiveRebounds = statResponse.OffensiveRebounds,
                            FreeThrowsAttempted = statResponse.FreeThrowsAttempted,
                            FreeThrowsMade = statResponse.FreeThrowsMade,
                            FreeThrowPercentage = statResponse.FreeThrowPercentage,
                            FieldGoalsAttempted = statResponse.FieldGoalsAttempted,
                            FieldGoalsMade = statResponse.FieldGoalsMade,
                            FieldGoalPercentage = statResponse.FieldGoalPercentage,
                            ThreePointersAttempted = statResponse.ThreePointersAttempted,
                            ThreePointersMade = statResponse.ThreePointersMade,
                            ThreePointerPercentage = statResponse.ThreePointerPercentage,
                            Steals = statResponse.Steals,
                            Blocks = statResponse.Blocks,
                            Turnovers = statResponse.Turnovers,
                            Minutes = statResponse.Minutes,
                            PlusMinus = statResponse.PlusMinus,

                        };


                        await _teamStatRepository.UpsertTeamStatAsync(stat);
                }
            }

                // Return all teams from the database
                return await _teamStatRepository.GetTeamStatAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while seeding teams: {ex.Message}");
                throw;
            }
        }

        public Task<bool> UpdateTeamStatAsync(TeamStat stat)
        {
            throw new NotImplementedException();
        }
    }

