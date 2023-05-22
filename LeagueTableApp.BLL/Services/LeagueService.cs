using AutoMapper;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueTableApp.BLL.Services;

public class LeagueService : ILeagueService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public LeagueService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void DeleteLeague(int leagueId)
    {
        throw new NotImplementedException();
    }

    public League GetLeague(int leagueId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<League> GetLeagues()
    {
        throw new NotImplementedException();
    }

    public League InsertLeague(League newLeague)
    {
        throw new NotImplementedException();
    }

    public void UpdateLeague(int leagueId, League updatedLeague)
    {
        throw new NotImplementedException();
    }
}
