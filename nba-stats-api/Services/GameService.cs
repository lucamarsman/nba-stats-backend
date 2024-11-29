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
            //Fetch NBA seasons from 2010 - current
            var seasons = await GetSeasonsAsync();
            var validNbaTeamAbbreviations = new HashSet<string>
            {
                "ATL", "BOS", "BKN", "CHA", "CHI", "CLE", "DAL", "DEN", "DET", "GSW",
                "HOU", "IND", "LAC", "LAL", "MEM", "MIA", "MIL", "MIN", "NOP", "NYK",
                "OKC", "ORL", "PHI", "PHX", "POR", "SAC", "SAS", "TOR", "UTA", "WAS"
            };

            // For each season since 2010, get and upsert every game into the db
            foreach (var season in seasons)
            {
                // Fetch games from the season
                var response = await _httpClient.GetAsync($"/schedule?Season={season}");
                response.EnsureSuccessStatusCode();

                // Parse the response JSON
                var responseData = await response.Content.ReadAsStringAsync();
                var cleanedJson = responseData.Replace("NaN", "null"); // Replaces NaN property values because it was breaking the Json serialization process
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Case-insensitive matching
                    AllowTrailingCommas = true,         // Handle JSON with trailing commas gracefully
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignore null values
                };
                var gameResponses = JsonSerializer.Deserialize<List<ScheduleResponse>>(cleanedJson, options);

                if (gameResponses == null || !gameResponses.Any())
                {
                    throw new Exception("No games were retrieved from the API.");
                }

                // For each game in the specified season, get the team abbreviations from the matchup field and extract the team ids, upsert game details
                foreach (var gameResponse in gameResponses)
                {
                    var matchUpParts = gameResponse.MatchUp.Split('@');
                    if (matchUpParts.Length != 2)
                    {
                        Console.WriteLine($"Invalid MatchUp format: {gameResponse.MatchUp}");
                        matchUpParts = gameResponse.MatchUp.Split("vs.");
                        continue;
                    }

                    var homeTeamAbbreviation = matchUpParts[1].Trim();
                    var awayTeamAbbreviation = matchUpParts[0].Trim();
                    var awayTeamId = await _teamRepository.GetTeamIdByAbbreviation(awayTeamAbbreviation);

                    // This ignores preseason matchups between NBA teams and Euroleague teams
                    if (!validNbaTeamAbbreviations.Contains(homeTeamAbbreviation) || !validNbaTeamAbbreviations.Contains(awayTeamAbbreviation))
                    {
                        Console.WriteLine($"Skipping game with non-NBA team: {gameResponse.MatchUp}");
                        continue;
                    }

                    var game = new Game
                    {
                        GameId = gameResponse.GameId,
                        Season = season,
                        HomeTeamId = gameResponse.HomeTeamId,
                        AwayTeamId = awayTeamId,
                        GameDate = DateTime.Parse(gameResponse.GameDate),
                        GameStatus = "Final",
                        MatchUp = gameResponse.MatchUp,
                        UpdatedAt = DateTime.UtcNow

                    };

                    await _gameRepository.UpsertGameAsync(game);
                }
            }

            // Return all teams from the database
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

