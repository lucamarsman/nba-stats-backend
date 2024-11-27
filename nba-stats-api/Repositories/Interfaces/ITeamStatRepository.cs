
public interface ITeamStatRepository
{
    Task<TeamStat> GetStatByIdAsync(int teamId);         
    Task<List<TeamStat>> GetTeamStatAsync();             
    Task<bool> UpdateTeamStatAsync(TeamStat stat);           
    Task AddTeamStatAsync(TeamStat stat);                   
    Task UpsertTeamStatAsync(TeamStat stat);                 
}

