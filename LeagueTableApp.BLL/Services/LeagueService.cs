using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Exceptions;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.DAL;
using Microsoft.EntityFrameworkCore;
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
        _context.Leagues.Remove(new DAL.Entities.League(null!) { Id = leagueId });
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Leagues.Any(p => p.Id == leagueId))
                throw new EntityNotFoundException("Nem található a bajnokság");
            else
                throw;
        }
    }

    public League GetLeague(int leagueId)
    {
        return _context.Leagues
            .ProjectTo<League>(_mapper.ConfigurationProvider)
            .SingleOrDefault(t => t.Id == leagueId) ?? throw new EntityNotFoundException("Nem található ilyen bajnokság!");
    }

    public IEnumerable<League> GetLeagues()
    {
        return _context.Leagues.ProjectTo<League>(_mapper.ConfigurationProvider).AsEnumerable();
    }

    public League InsertLeague(League newLeague)
    {
        var leagueFromEf = _mapper.Map<DAL.Entities.League>(newLeague);
        _context.Leagues.Add(leagueFromEf);
        _context.SaveChanges();
        return GetLeague(leagueFromEf.Id);
    }

    public void UpdateLeague(int leagueId, League updatedLeague)
    {
        var leagueFromEf = _mapper.Map<DAL.Entities.League>(updatedLeague);
        leagueFromEf.Id = leagueId;
        _context.Attach(leagueFromEf).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Leagues.Any(p => p.Id == leagueId))
                throw new EntityNotFoundException("Nem található a bajnokság");
            else
                throw;
        }

    }
}
