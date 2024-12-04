using nba_stats_api.API;
using nba_stats_api.Models;
using Sprache;
using System.Text.Json;
using System.Text.Json.Serialization;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly HttpClient _httpClient;
    private List<string>? _seasons;

    public GameService(IGameRepository gameRepository, ITeamRepository teamRepository, HttpClient httpClient)
    {
        _gameRepository = gameRepository;
        _teamRepository = teamRepository;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000");
    }

    public async Task<Game> GetGameByIdAsync(int gameId)
    {
        // Check the database first
        var existingGame = await _gameRepository.GetGameByIdAsync(gameId);
        if (existingGame != null && existingGame.UpdatedAt > DateTime.UtcNow.AddDays(-1))
        {
            return existingGame; // Return the game if the data is up-to-date
        }

        return null;
    }

    public async Task<List<Game>> SeedGames()
    {
        try
        {
            var seasons = await GetSeasonsAsync();
            var validNbaTeamAbbreviations = new HashSet<string>
        {
            "ATL", "BOS", "BKN", "CHA", "CHI", "CLE", "DAL", "DEN", "DET", "GSW",
            "HOU", "IND", "LAC", "LAL", "MEM", "MIA", "MIL", "MIN", "NOP", "NYK",
            "OKC", "ORL", "PHI", "PHX", "POR", "SAC", "SAS", "TOR", "UTA", "WAS"
        };

            foreach (var season in seasons)
            {
                var response = await _httpClient.GetAsync($"/schedule?Season={season}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                var cleanedJson = responseData.Replace("NaN", "null");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var gameResponses = JsonSerializer.Deserialize<List<ScheduleResponse>>(cleanedJson, options);

                if (gameResponses == null || !gameResponses.Any())
                {
                    throw new Exception("No games were retrieved from the API.");
                }

                foreach (var gameResponse in gameResponses)
                {
                    var matchUpParts = gameResponse.MatchUp.Contains("@")
                        ? gameResponse.MatchUp.Split('@')
                        : gameResponse.MatchUp.Split("vs.");

                    if (matchUpParts.Length != 2)
                    {
                        Console.WriteLine($"Invalid MatchUp format: {gameResponse.MatchUp}");
                        continue;
                    }

                    var awayTeamAbbreviation = matchUpParts[0].Trim();
                    var homeTeamAbbreviation = matchUpParts[1].Trim();

                    if (!validNbaTeamAbbreviations.Contains(homeTeamAbbreviation) ||
                        !validNbaTeamAbbreviations.Contains(awayTeamAbbreviation))
                    {
                        Console.WriteLine($"Skipping game with non-NBA team: {gameResponse.MatchUp}");
                        continue;
                    }

                    var awayTeamId = await _teamRepository.GetTeamIdByAbbreviation(awayTeamAbbreviation);
                    var homeTeamId = await _teamRepository.GetTeamIdByAbbreviation(homeTeamAbbreviation);

                    if (homeTeamId == null || awayTeamId == null)
                    {
                        Console.WriteLine($"Skipping game due to missing team IDs: {gameResponse.MatchUp}");
                        continue;
                    }

                    var game = new Game
                    {
                        GameId = gameResponse.GameId,
                        Season = season,
                        HomeTeamId = homeTeamId,
                        AwayTeamId = awayTeamId,
                        GameDate = DateTime.Parse(gameResponse.GameDate),
                        GameStatus = "Final",
                        MatchUp = gameResponse.MatchUp,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _gameRepository.UpsertGameAsync(game);
                }
            }

            return await _gameRepository.GetAllGamesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while seeding games: {ex.Message}");
            throw;
        }
    }


    public Task<bool> UpdateGameAsync(Game game)
    {
        throw new NotImplementedException();
    }

    public Task AddGameAsync(Game game)
    {
        throw new NotImplementedException();
    }

    public Task<List<Game>> GetAllGamesAsync()
    {
        throw new NotImplementedException();
    }

    //This method fetches games using a date range, game info is fetched from the external api and cached if not already contained in the local db
    public async Task<List<Game>> GetUpcomingGamesAsync(DateTime start, DateTime end)
    {
        // Fetch games within the date range from the database
        var existingGames = await _gameRepository.GetGamesByDateRangeAsync(start, end);

        // Find missing dates
        var missingDates = Enumerable
            .Range(0, (end - start).Days + 1)
            .Select(offset => start.AddDays(offset))
            .Where(date => !existingGames.Any(game => game.GameDate.Date == date.Date))
            .ToList();

        if (missingDates.Any())
        {
            // Fetch missing games from the external API
            foreach (var date in missingDates)
            {
                var response = await _httpClient.GetAsync($"/games?GameDate={date:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(responseData);
                var gameHeadersJson = document.RootElement.GetProperty("GameHeader");

                // Deserialize the GameHeader section into a list
                var gameHeaders = JsonSerializer.Deserialize<List<GameHeaderResponse>>(gameHeadersJson.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                foreach (var gameResponse in gameHeaders)
                {
                    var matchup = gameResponse.GameCode;
                    string teamsPart = matchup.Split('/')[1];
                    string awayTeam = teamsPart.Substring(0, 3);
                    string homeTeam = teamsPart.Substring(3, 3);
                    string formattedMatchup = $"{awayTeam} @ {homeTeam}";

                    var game = new Game
                    {
                        GameId = gameResponse.GameId,
                        HomeTeamId = gameResponse.HomeTeamId,
                        AwayTeamId = gameResponse.AwayTeamId,
                        GameDate = DateTime.Parse(gameResponse.GameDate),
                        GameStatus = gameResponse.GameStatus,
                        MatchUp = formattedMatchup,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _gameRepository.UpsertGameAsync(game);
                }
            }

            // Fetch updated games from the database
            existingGames = await _gameRepository.GetGamesByDateRangeAsync(start, end);
        }

        return existingGames;
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

}

