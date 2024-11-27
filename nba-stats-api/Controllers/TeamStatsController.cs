
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
            foreach (var perMode in new[] { "Totals", "PerGame", "Per36", "PerPossession", "Per100Possessions" })
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

    [HttpGet]
    public async Task<IActionResult> GetTeamStatsAsync()
    {
        var teams = await _teamStatService.GetTeamStatsAsync();
        return Ok(teams);
    }

}

