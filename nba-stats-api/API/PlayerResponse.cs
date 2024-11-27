using System.Text.Json.Serialization;

namespace nba_stats_api.API
{
    public class PlayerApiResponse
    {
        [JsonPropertyName("AvailableSeasons")]
        public List<AvailableSeason> AvailableSeasons { get; set; }

        [JsonPropertyName("CommonPlayerInfo")]
        public List<CommonPlayerInfo> CommonPlayerInfo { get; set; }

        [JsonPropertyName("PlayerHeadlineStats")]
        public List<PlayerHeadlineStat> PlayerHeadlineStats { get; set; }
    }


    public class AvailableSeason
    {
        [JsonPropertyName("SEASON_ID")]
        public string? SeasonId { get; set; }
    }

    public class CommonPlayerInfo
    {
        [JsonPropertyName("PERSON_ID")]
        public int PersonId { get; set; }

        [JsonPropertyName("FIRST_NAME")]
        public string? FirstName { get; set; }

        [JsonPropertyName("LAST_NAME")]
        public string? LastName { get; set; }

        [JsonPropertyName("DISPLAY_FIRST_LAST")]
        public string? DisplayFirstLast { get; set; }

        [JsonPropertyName("POSITION")]
        public string? Position { get; set; }

        [JsonPropertyName("HEIGHT")]
        public string? Height { get; set; }

        [JsonPropertyName("WEIGHT")]
        public string? Weight { get; set; }

        [JsonPropertyName("BIRTHDATE")]
        public string? BirthDate { get; set; }

        [JsonPropertyName("COUNTRY")]
        public string? Country { get; set; }

        [JsonPropertyName("LAST_AFFILIATION")]
        public string? LastAffiliation { get; set; }

        [JsonPropertyName("TEAM_NAME")]
        public string? TeamName { get; set; }

        [JsonPropertyName("TEAM_CITY")]
        public string? TeamCity { get; set; }

        [JsonPropertyName("TEAM_ABBREVIATION")]
        public string? TeamAbbreviation { get; set; }

        [JsonPropertyName("TEAM_ID")]
        public int? TeamId { get; set; }

        [JsonPropertyName("DRAFT_YEAR")]
        public string? DraftYear { get; set; }

        [JsonPropertyName("DRAFT_ROUND")]
        public string? DraftRound { get; set; }

        [JsonPropertyName("DRAFT_NUMBER")]
        public string? DraftNumber { get; set; }

        [JsonPropertyName("SEASON_EXP")]
        public int? SeasonExp { get; set; }

        [JsonPropertyName("JERSEY")]
        public string? Jersey { get; set; }

        [JsonPropertyName("ROSTERSTATUS")]
        public string? RosterStatus { get; set; }

        [JsonPropertyName("NBA_FLAG")]
        public string? IsNBAPlayer { get; set; }

        [JsonPropertyName("GREATEST_75_FLAG")]
        public string? IsGreatest75 { get; set; }
    }

    public class PlayerHeadlineStat
    {
        [JsonPropertyName("PLAYER_ID")]
        public int? PlayerId { get; set; }

        [JsonPropertyName("PLAYER_NAME")]
        public string? PlayerName { get; set; }

        [JsonPropertyName("PTS")]
        public double? Points { get; set; }

        [JsonPropertyName("AST")]
        public double? Assists { get; set; }

        [JsonPropertyName("REB")]
        public double? Rebounds { get; set; }

        [JsonPropertyName("PIE")]
        public double? PlayerImpactEstimate { get; set; }

        [JsonPropertyName("TimeFrame")]
        public string? TimeFrame { get; set; }
    }


}
