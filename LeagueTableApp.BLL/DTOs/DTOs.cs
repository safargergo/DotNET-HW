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
        public string? Coach { get; init; }
        public string? Captain { get; init; }
        public string Players { get; init; } = null!;
        public int LeagueId { get; init; }
        //public League? League { get; init; }
    }
    public record Match
    {
        public int Id { get; init; }
        public string Location { get; init; } = null!;
        public int LeagueId { get; init; }
        public League? League { get; init; }
        public int? HomeTeamId { get; init; }
        public Team? HomeTeam { get; init; }
        public int? ForeignTeamId { get; init; }
        public Team? ForeignTeam { get; init; }
        public double HomeTeamScore { get; init; }
        public double ForeignTeamScore { get; init; }
        public bool IsEnded { get; init; }
    }
    public record League
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        //public ICollection<Team> Teams { get; } = new List<Team>();
        //public ICollection<Match> Matches { get; } = new List<Match>();
    }
}
