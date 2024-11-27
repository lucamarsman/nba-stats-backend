using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

public class PlayerStat
{
    [JsonPropertyName("PLAYER_ID")]
    public int PlayerId { get; set; }

    [JsonPropertyName("PLAYER_NAME")]
    public string PlayerName { get; set; }

    [JsonPropertyName("TEAM_ID")]
    public int? TeamId { get; set; }

    public string SeasonType { get; set; } = "Regular Season"; // Example: 'Regular Season', 'Playoffs'
    public string PerMode { get; set; } // Example: 'PerGame', 'Totals'

    [JsonPropertyName("GP")]
    public int GamesPlayed { get; set; }

    [JsonPropertyName("PTS")]
    public float Points { get; set; }

    [JsonPropertyName("AST")]
    public float Assists { get; set; }

    [JsonPropertyName("REB")]
    public float Rebounds { get; set; }

    [JsonPropertyName("DREB")]
    public float DefensiveRebounds { get; set; }

    [JsonPropertyName("OREB")]
    public float OffensiveRebounds { get; set;}

    [JsonPropertyName("FTA")]
    public float FreeThrowsAttempted { get; set; }

    [JsonPropertyName("FTM")]
    public float FreeThrowsMade {  get; set; }

    [JsonPropertyName("FT_PCT")]
    public float FreeThrowPercentage { get; set; }

    [JsonPropertyName("FGA")]
    public float FieldGoalsAttempted { get; set; }

    [JsonPropertyName("FGM")]
    public float FieldGoalsMade { get; set; }

    [JsonPropertyName("FG_PCT")]
    public float FieldGoalPercentage { get; set; }

    [JsonPropertyName("FG3A")]
    public float ThreePointersAttempted { get; set; }

    [JsonPropertyName("FG3M")]
    public float ThreePointersMade { get; set; }

    [JsonPropertyName("FG3_PCT")]
    public float ThreePointerPercentage { get; set; }

    [JsonPropertyName("STL")]
    public float Steals { get; set; }

    [JsonPropertyName("BLK")]
    public float Blocks { get; set; }

    [JsonPropertyName("TOV")]
    public float Turnovers { get; set; }

    [JsonPropertyName("MIN")]
    public float Minutes { get; set; }

    [JsonPropertyName("PLUS_MINUS")]
    public float PlusMinus { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
