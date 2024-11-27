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
            var statsTotalsSeeded = await _playerStatService.SeedStats("Totals");
            var statsPerGameSeeded = await _playerStatService.SeedStats("PerGame");
            var statsMinutesPerSeeded = await _playerStatService.SeedStats("MinutesPer");
            var statsPer48Seeded = await _playerStatService.SeedStats("Per48");
            var statsPer40Seeded = await _playerStatService.SeedStats("Per40");
            var statsPer36Seeded = await _playerStatService.SeedStats("Per36");
            var statsPerMinuteSeeded = await _playerStatService.SeedStats("PerMinute");
            var statsPerPossessionSeeded = await _playerStatService.SeedStats("PerPossession");
            var statsPerPlaySeeded = await _playerStatService.SeedStats("PerPlay");
            var statsPer100PossessionsSeeded = await _playerStatService.SeedStats("Per100Possessions");
            var statsPer100PlaysSeeded = await _playerStatService.SeedStats("Per100Plays");


            return Ok("Player Stats Seeded");
            
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

