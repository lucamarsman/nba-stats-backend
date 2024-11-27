using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedPlayers()
    {
        try
        {
            var gamesSeeded = await _gameService.SeedGames();
            return Ok(gamesSeeded);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("GetUpcomingGames")]
    public async Task<IActionResult> GetUpcomingGamesAsync([FromBody] DateRangeRequest dateRange)
    {
        try
        {
            var games = await _gameService.GetUpcomingGamesAsync(dateRange.Start, dateRange.End);
            return Ok(games);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public class DateRangeRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

