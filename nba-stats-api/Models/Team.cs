namespace nba_stats_api.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Abbreviation { get; set; }
        public string City { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string State {  get; set; }
        public int YearFounded { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
