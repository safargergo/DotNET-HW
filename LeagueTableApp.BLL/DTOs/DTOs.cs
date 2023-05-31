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
        public int Id { get; set; }
        //public string Location { get; set; } = null!;
        public int LeagueId { get; set; }
        public League? League { get; set; }
        public int? HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }
        public int? ForeignTeamId { get; set; }
        public Team? ForeignTeam { get; set; }
        public double HomeTeamScore { get; set; }
        public double ForeignTeamScore { get; set; }
        public bool IsEnded { get; set; }
    }
    public record League
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        //public ICollection<Team> Teams { get; } = new List<Team>();
        //public ICollection<Match> Matches { get; } = new List<Match>();
    }
    public record PointsTable
    {
        //public List<string>? Teams { get; init; }
        //public List<int>? Points { get; init; }
        //public Dictionary<string, int> teamPointPairs { get; init; } = null!;
        public List<KeyValuePair<string, int>> teamPointPairs { get; set; } = null!;
    }
}
