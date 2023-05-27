﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueTableApp.DAL.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public Match(string location)
        {
            Location = location;
        }

        public int? MatchsLeagueId { get; set; }
        public League? MatchsLeague { get; set; }
        public int? HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; } = null!;
        public int? ForeignTeamId { get; set; }
        public Team? ForeignTeam { get; set; } = null!;
        public double HomeTeamScore { get; set; }
        public double ForeignTeamScore { get; set; }
        public bool IsEnded { get; set; }
    }
}
