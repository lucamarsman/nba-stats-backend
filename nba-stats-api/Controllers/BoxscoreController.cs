using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/boxscore")]
public class BoxscoreController : ControllerBase
{
    private readonly IBoxscoreService _boxscoreService;

    public BoxscoreController(IBoxscoreService boxscoreService)
    {
        _boxscoreService = boxscoreService;
    }



    [HttpPost("seed")]
    public async Task<IActionResult> SeedPlayers()
    {
        try
        {
            var boxscoresSeeded = await _boxscoreService.SeedBoxscores();
            return Ok(boxscoresSeeded);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

