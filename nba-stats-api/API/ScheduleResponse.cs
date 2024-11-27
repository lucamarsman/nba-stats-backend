
using System.Text.Json.Serialization;

public class ScheduleResponse
    {
        [JsonPropertyName("GAME_ID")]
        public string GameId { get; set; }

        [JsonPropertyName("TEAM_ID")]
        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        [JsonPropertyName("GAME_DATE")]
        public string GameDate { get; set; }

        public string GameStatus { get; set; }

        [JsonPropertyName("MATCHUP")]
        public string MatchUp { get; set; }

        public DateTime UpdatedAt { get; set; }
}

