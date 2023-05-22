using System;
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

        public int HomeTeamId { get; set; }
        public int ForeignTeamId { get; set; }
        public double HomeTeamScore { get; set; }
        public double ForeignTeamScore { get; set; }
        public bool IsEnded { get; set; }
    }
}
