using nba_stats_api.API;
using System.Text.Json;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly HttpClient _httpClient;

    public PlayerService(IPlayerRepository playerRepository, HttpClient httpClient)
    {
        _playerRepository = playerRepository;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000");
    }

    public async Task<Player> GetPlayerByIdAsync(int playerId)
    {
        // Check the database first
        var existingPlayer = await _playerRepository.GetPlayerByIdAsync(playerId);
        if (existingPlayer != null && existingPlayer.UpdatedAt > DateTime.UtcNow.AddDays(-1))
        {
            return existingPlayer; // Return the player if the data is up-to-date
        }

        try
        {
            var response = await _httpClient.GetAsync($"/player-info?PlayerId={playerId}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw JSON Response: {responseData}");
            // Deserialize into the PlayerApiResponse class
            var playerApiResponse = JsonSerializer.Deserialize<PlayerApiResponse>(responseData);
            Console.WriteLine($"Deserialized Response: {JsonSerializer.Serialize(playerApiResponse)}");

            if (playerApiResponse == null || playerApiResponse.CommonPlayerInfo.Count == 0)
            {
                throw new Exception("Player data is missing or invalid.");
            }

            // Check if PlayerHeadlineStats is empty or null
            var commonPlayerInfo = playerApiResponse.CommonPlayerInfo.FirstOrDefault();
            var playerHeadlineStats = playerApiResponse.PlayerHeadlineStats.FirstOrDefault();

            var player = new Player
            {
                PlayerId = commonPlayerInfo.PersonId,
                FirstName = commonPlayerInfo.FirstName,
                LastName = commonPlayerInfo.LastName,
                DisplayName = commonPlayerInfo.DisplayFirstLast,
                Position = commonPlayerInfo.Position,
                Height = commonPlayerInfo.Height,
                Weight = commonPlayerInfo.Weight,
                BirthDate = commonPlayerInfo.BirthDate,
                Country = commonPlayerInfo.Country,
                LastAffiliation = commonPlayerInfo.LastAffiliation,
                TeamId = commonPlayerInfo.TeamId,
                TeamName = commonPlayerInfo.TeamName,
                TeamCity = commonPlayerInfo.TeamCity,
                TeamAbbreviation = commonPlayerInfo.TeamAbbreviation,
                DraftYear = commonPlayerInfo.DraftYear,
                DraftRound = commonPlayerInfo.DraftRound,
                DraftNumber = commonPlayerInfo.DraftNumber == "Undrafted" ? null : commonPlayerInfo.DraftNumber,
                SeasonExperience = commonPlayerInfo.SeasonExp,
                JerseyNumber = commonPlayerInfo.Jersey,
                PointsPerGame = playerHeadlineStats?.Points ?? 0, // Default to 0 if PlayerHeadlineStats is empty
                ReboundsPerGame = playerHeadlineStats?.Rebounds ?? 0, // Default to 0 if PlayerHeadlineStats is empty
                AssistsPerGame = playerHeadlineStats?.Assists ?? 0, // Default to 0 if PlayerHeadlineStats is empty
                PlayerImpactEstimate = playerHeadlineStats?.PlayerImpactEstimate ?? 0, // Default to 0 if PlayerHeadlineStats is empty
                IsActive = commonPlayerInfo.RosterStatus == "Active",
                IsNBAPlayer = commonPlayerInfo.IsNBAPlayer == "Y",
                IsGreatest75 = commonPlayerInfo.IsGreatest75 == "Y",
                UpdatedAt = DateTime.UtcNow
            };

            if(commonPlayerInfo.RosterStatus == "Active")
            {
                await _playerRepository.UpsertPlayerAsync(player);
            }

            return player;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    // Seed player table
    public async Task<List<Player>> SeedPlayers()
    {

        var response = await _httpClient.GetAsync($"/players");
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(responseData);
        var root = document.RootElement;

        // Extract only the player IDs
        var playerIds = root.EnumerateArray()
                            .Select(player => player.GetProperty("id").GetInt32())
                            .ToList();

        foreach (var id in playerIds)
        {
            await GetPlayerByIdAsync(id);
        }

        return await _playerRepository.GetAllPlayersAsync();
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _playerRepository.GetAllPlayersAsync();
    }

    public async Task<bool> UpdatePlayerAsync(Player player)
    {
        return await _playerRepository.UpdatePlayerAsync(player);
    }

    public async Task AddPlayerAsync(Player player)
    {
        await _playerRepository.AddPlayerAsync(player);
    }
}


