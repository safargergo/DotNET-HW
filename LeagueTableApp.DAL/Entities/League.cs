using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueTableApp.DAL.Entities
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public League(string name)
        {
            Name = name;
        }

        public ICollection<Team> Teams { get; } = new List<Team>();
        public ICollection<Match> Matches { get; } = new List<Match>();
        public bool IsDeleted { get; set; } = false;
    }
}
