using LeagueTableApp.BLL.DTOs;

namespace LeagueTableApp.BLL.Interfaces;
public interface ILeagueService
{
    public League GetLeague(int leagueId);
    public IEnumerable<League> GetLeagues();
    public League InsertLeague(League newLeague);
    public void UpdateLeague(int leagueId, League updatedLeague);
    public void DeleteLeague(int leagueId);
}
