
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/team-stats")]
public class TeamStatsController : ControllerBase
{
    private readonly ITeamStatService _teamStatService;

    public TeamStatsController(ITeamStatService teamStatService)
    {
        _teamStatService = teamStatService;
    }


    [HttpPost("seed")]
    public async Task<IActionResult> SeedPlayers()
    {
        try
        {
            foreach (var perMode in new[] { "Totals", "PerGame", "MinutesPer", "Per48", "Per40", "Per36", "PerMinute", "PerPossession", "PerPlay", "Per100Possessions", "Per100Plays" })
            {
                await _teamStatService.SeedStats(perMode);
            }

            return Ok("Team Stats Seeded");

        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}

