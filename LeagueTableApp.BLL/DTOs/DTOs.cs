using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueTableApp.BLL.DTOs
{
    public record Team
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string Coach { get; init; } = null!;
        public string Captain { get; init; } = null!;
        public string Players { get; init; } = null!;
    }
    public record Match
    {
        public int Id { get; init; }
        public string Location { get; init; } = null!;
        public int? MatchsLeagueId { get; init; }
        public League? MatchsLeague { get; init; }
        public int? HomeTeamId { get; init; }
        public Team? HomeTeam { get; init; }
        public int? ForeignTeamId { get; init; }
        public Team? ForeignTeam { get; init; }
        public double HomeTeamScore { get; init; }
        public double ForeignTeamScore { get; init; }
        public bool IsEnded { get; init; } = false;
    }
    public record League
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public List<Team> Team { get; init; } = new List<Team>();
        //public List<Match>? Match { get; init; } 
    }
}
