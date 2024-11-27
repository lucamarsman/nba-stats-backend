
public class Game
{
    public string GameId { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public DateTime GameDate { get; set; }
    public string GameStatus { get; set; }
    public string MatchUp {  get; set; }

    public DateTime UpdatedAt { get; set; }
}
