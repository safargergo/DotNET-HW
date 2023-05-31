using LeagueTableApp.BLL.DTOs;

namespace LeagueTableApp.BLL.Interfaces;
public interface ITeamService
{
    public Team GetTeam(int teamId);
    public IEnumerable<Team> GetTeams();
    public Team InsertTeam(Team newTeam);
    public void UpdateTeam(int teamId, Team updatedTeam);
    public void DeleteTeam(int teamId);
    public IEnumerable<Match> ListPlayedMatchesOfTeam(int teamId);
}
