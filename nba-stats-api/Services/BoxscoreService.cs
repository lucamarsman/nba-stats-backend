

using System.Text.Json;

public class BoxscoreService : IBoxscoreService
{
    private readonly IBoxscoreRepository _boxscoreRepository;
    private readonly IGameRepository _gameRepository;
    private readonly HttpClient _httpClient;

    public BoxscoreService(IBoxscoreRepository boxscoreRepository,IGameRepository gameRepository, HttpClient httpClient)
    {
        _boxscoreRepository = boxscoreRepository;
        _gameRepository = gameRepository;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000");
    }



    public async Task AddBoxscoreAsync(Boxscore boxscore)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Boxscore>> GetAllBoxscoresAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Boxscore> GetBoxscoreByIdAsync(int gameId)
    {
        // Check the database first
        var existingGame = await _boxscoreRepository.GetBoxscoreByIdAsync(gameId);
        if (existingGame != null && existingGame.UpdatedAt > DateTime.UtcNow.AddDays(-1))
        {
            return existingGame; // Return the game if the data is up-to-date
        }

        return null;
    }

    public async Task<List<Boxscore>> SeedBoxscores()
    {

        try
        {
            var gameIds = (await _gameRepository.GetAllGamesAsync())
            .Select(game => game.GameId)
            .ToList();

            foreach (var id in gameIds)
            {
                var response = await _httpClient.GetAsync($"/boxscore?gameId={id}");
                response.EnsureSuccessStatusCode();

                // Parse the response JSON
                var responseData = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(responseData);
                var statsJson = document.RootElement.GetProperty("PlayerStats");

                var playerStats = JsonSerializer.Deserialize<List<Boxscore>>(statsJson.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                foreach (var statline in playerStats)
                {
                    Console.WriteLine(statline);

                    var boxscore = new Boxscore
                    {
                        GameId = statline.GameId,
                        PlayerId = statline.PlayerId,
                        playerName = statline.playerName,
                        gameComment = statline.gameComment,
                        position = statline.position,
                        minutes = statline.minutes,
                        assists = statline.assists,
                        rebounds = statline.rebounds,
                        blocks = statline.blocks,
                        turnovers = statline.turnovers,
                        points = statline.points,
                        steals = statline.steals,
                        plusMinus = statline.plusMinus,
                        fieldGoalsAttempted = statline.fieldGoalsAttempted,
                        fieldGoalsMade = statline.fieldGoalsMade,
                        fieldGoalPercentage = statline.fieldGoalPercentage,
                        threePointersAttempted = statline.threePointersAttempted,
                        threePointersMade = statline.threePointersMade,
                        threePointerPercentage = statline.threePointerPercentage

                    };

                    await _boxscoreRepository.UpsertBoxscoreAsync(boxscore);

                }

            }

            return await _boxscoreRepository.GetAllBoxscoresAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while seeding teams: {ex.Message}");
            throw;
        }

        
      
    }

    public async Task<bool> UpdateBoxscoreAsync(Game game)
    {
        throw new NotImplementedException();
    }
}

