using System.Text.Json.Serialization;

public class Boxscore
{
    [JsonPropertyName("GAME_ID")]
    public string GameId { get; set; }

    [JsonPropertyName("PLAYER_ID")]
    public int PlayerId { get; set; }

    [JsonPropertyName("PLAYER_NAME")]
    public string playerName { get; set; }

    [JsonPropertyName("COMMENT")]
    public string? gameComment { get; set; }

    [JsonPropertyName("START_POSITION")]
    public string? position { get; set; }

    [JsonPropertyName("MIN")]
    public string? minutes { get; set; }

    [JsonPropertyName("AST")]
    public int? assists { get; set; }

    [JsonPropertyName("REB")]
    public int? rebounds { get; set; }

    [JsonPropertyName("BLK")]
    public int? blocks { get; set; }

    [JsonPropertyName("TO")]
    public int? turnovers { get; set; }

    [JsonPropertyName("PTS")]
    public double? points { get; set; }

    [JsonPropertyName("STL")]
    public int? steals { get; set; }

    [JsonPropertyName("PLUS_MINUS")]
    public double? plusMinus { get; set; }

    [JsonPropertyName("FGA")]
    public int? fieldGoalsAttempted { get; set; }

    [JsonPropertyName("FGM")]
    public int? fieldGoalsMade { get; set; }

    [JsonPropertyName("FG_PCT")]
    public double? fieldGoalPercentage { get; set; }

    [JsonPropertyName("FG3A")]
    public int? threePointersAttempted { get; set; }

    [JsonPropertyName("FG3M")]
    public int? threePointersMade { get; set; }

    [JsonPropertyName("FG3_PCT")]
    public double? threePointerPercentage { get; set; }

    public DateTime UpdatedAt { get; set; }

}

