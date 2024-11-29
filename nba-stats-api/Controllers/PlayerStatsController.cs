using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/player-stats")]
public class PlayerStatsController : ControllerBase
{
    private readonly IPlayerStatService _playerStatService;

    public PlayerStatsController(IPlayerStatService playerStatService)
    {
        _playerStatService = playerStatService;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedPlayers()
    {
        try
        {
            foreach (var perMode in new[] { "Totals", "PerGame", "Per36", "PerPossession", "Per100Possessions" })
            {
                await _playerStatService.SeedStats(perMode);
            }

            return Ok("Team Stats Seeded");

        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

