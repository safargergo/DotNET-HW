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
        public List<string> Players { get; init; } = null!;
        public List<League> Leagues { get; init; } = null!; 
        public List<Team> Teams { get; init; } = null!;
    }
    public record Match
    {
        public int Id { get; init; }
        public string Location { get; init; } = null!;
        public int HomeTeamId { get; init; }
        public int ForeignTeamId { get; init; }
        public double HomeTeamScore { get; init; }
        public double ForeignTeamScore { get; init; }
        public bool IsEnded { get; init; }
    }
    public record League
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public List<Team> Teams { get; init; } = null!;
        public List<Match> Matches { get; init; } = null!;
    }
}
