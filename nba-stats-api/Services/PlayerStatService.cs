using System.Text.Json;

public class PlayerStatService : IPlayerStatService
{
    private readonly IPlayerStatRepository _playerStatRepository;
    private readonly HttpClient _httpClient;

    public PlayerStatService(IPlayerStatRepository playerStatRepository, HttpClient httpClient)
    {
        _playerStatRepository = playerStatRepository;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000");
    }

    public Task AddStatAsync(PlayerStat stat)
    {
        throw new NotImplementedException();
    }

    public Task<List<PlayerStat>> GetPlayerStatsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PlayerStat> GetStatByIdAsync(int playerId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PlayerStat>> SeedStats(String permode)
    {
        try
        {
            // Fetch data from the API
            var response = await _httpClient.GetAsync($"/stats/players?PerMode={permode}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(responseData);
            var statsJson = document.RootElement.GetProperty("LeagueDashPlayerStats");

            // Deserialize the LeagueDashPlayerStats section into a list
            var leagueDashPlayerStats = JsonSerializer.Deserialize<List<PlayerStat>>(statsJson.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Map and upsert teams
            foreach (var statResponse in leagueDashPlayerStats)
            {
                
                var stat = new PlayerStat
                {
                    PlayerId = statResponse.PlayerId,
                    PlayerName = statResponse.PlayerName,
                    TeamId = statResponse.TeamId,
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

                await _playerStatRepository.UpsertPlayerStatAsync(stat);
            }

            // Return all teams from the database
            return await _playerStatRepository.GetPlayerStatAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while seeding teams: {ex.Message}");
            throw;
        }
    }

    public Task<bool> UpdatePlayerStatAsync(PlayerStat stat)
    {
        throw new NotImplementedException();
    }
}

