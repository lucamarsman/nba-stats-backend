
using System.Text.Json.Serialization;

public class GameHeaderResponse
{
    [JsonPropertyName("GAME_ID")]
    public string GameId { get; set; }

    [JsonPropertyName("HOME_TEAM_ID")]
    public int HomeTeamId { get; set; }

    [JsonPropertyName("VISITOR_TEAM_ID")]
    public int AwayTeamId { get; set; }

    [JsonPropertyName("GAME_DATE_EST")]
    public string GameDate { get; set; }

    [JsonPropertyName("GAMECODE")]
    public string GameCode { get; set; }

    [JsonPropertyName("GAME_STATUS_TEXT")]
    public string GameStatus { get; set; } 
}

