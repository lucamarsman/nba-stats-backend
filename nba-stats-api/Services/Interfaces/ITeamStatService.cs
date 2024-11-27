

public interface ITeamStatService
{
    Task<TeamStat> GetStatByIdAsync(int teamId);         
    Task<List<TeamStat>> GetTeamStatsAsync();             
    Task<bool> UpdateTeamStatAsync(TeamStat stat);           
    Task AddStatAsync(TeamStat stat);                  
    Task<List<TeamStat>> SeedStats(string permode);              
}

