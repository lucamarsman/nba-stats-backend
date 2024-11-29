using Microsoft.AspNetCore.Mvc;
using nba_stats_api.Models;

[ApiController]
[Route("api/players")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    // Get player by ID
    [HttpGet("{playerId}")]
    public async Task<IActionResult> GetPlayer(int playerId)
    {
        var player = await _playerService.GetPlayerByIdAsync(playerId);
        if (player == null)
            return NotFound($"Player with ID {playerId} not found.");

        return Ok(player);
    }

    // Get all players
    [HttpGet]
    public async Task<IActionResult> GetAllPlayers()
    {
        var players = await _playerService.GetAllPlayersAsync();
        return Ok(players);
    }

    // Update a player's information
    [HttpPut("{playerId}")]
    public async Task<IActionResult> UpdatePlayer(int playerId, [FromBody] Player updatedPlayer)
    {
        if (playerId != updatedPlayer.PlayerId)
            return BadRequest("Player ID mismatch.");

        var result = await _playerService.UpdatePlayerAsync(updatedPlayer);
        if (!result)
            return NotFound($"Player with ID {playerId} not found.");

        return NoContent();
    }

    // Add a new player
    [HttpPost]
    public async Task<IActionResult> AddPlayer([FromBody] Player newPlayer)
    {
        await _playerService.AddPlayerAsync(newPlayer);
        return CreatedAtAction(nameof(GetPlayer), new { playerId = newPlayer.PlayerId }, newPlayer);
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedPlayers()
    {
        try
        {
            var playersSeeded = await _playerService.SeedPlayers();
            return Ok(playersSeeded);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{playerId}/details")]
    public async Task<IActionResult> GetPlayerAwardsAsync(int playerId)
    {
        var playerAwards = await _playerService.GetPlayerAwardsAsync(playerId);

        if (playerAwards == null || !playerAwards.Any())
        {
            return NotFound($"No awards found for player with ID {playerId}.");
        }

        return Ok(playerAwards);
    }



}

