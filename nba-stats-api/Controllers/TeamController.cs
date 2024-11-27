using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/teams")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedTeams()
    {
        try
        {
            var playersSeeded = await _teamService.SeedTeams();
            return Ok(playersSeeded);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeams()
    {
        var teams = await _teamService.GetAllTeamsAsync();
        return Ok(teams);
    }

}

