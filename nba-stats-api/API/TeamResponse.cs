using System.Text.Json.Serialization;

namespace nba_stats_api.API
{
    public class TeamResponse
    {
        [JsonPropertyName("id")]
        public int TeamId { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("nickname")]
        public string NickName { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("year_founded")]
        public int YearFounded { get; set; }
    }
}
