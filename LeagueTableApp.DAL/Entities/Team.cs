using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueTableApp.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Team(string name)
        {
            Name = name;
        }

        public ICollection<string> Players { get; } = new List<string>();
        public ICollection<League> Leagues { get; } = new List<League>();   //???
        public ICollection<Match> Matches { get; } = new List<Match>();    //???
    }
}
