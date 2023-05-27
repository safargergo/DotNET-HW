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

public class MatchService : IMatchService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public MatchService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void DeleteMatch(int matchId)
    {
        _context.Matches.Remove(new DAL.Entities.Match(null!) { Id = matchId });
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Matches.Any(p => p.Id == matchId))
                throw new EntityNotFoundException("Nem található a meccs!");
            else
                throw;
        }
    }

    public Match GetMatch(int matchId)
    {
        return _context.Matches
            .ProjectTo<Match>(_mapper.ConfigurationProvider)
            .SingleOrDefault(t => t.Id == matchId) ?? throw new EntityNotFoundException("Nem található ilyen meccs!");
    }

    public IEnumerable<Match> GetMatches()
    {
        return _context.Matches.ProjectTo<Match>(_mapper.ConfigurationProvider).AsEnumerable();
    }

    public Match InsertMatch(Match newMatch)
    {
        var matchFromEf = _mapper.Map<DAL.Entities.Match>(newMatch);
        _context.Matches.Add(matchFromEf);
        _context.SaveChanges();
        return GetMatch(matchFromEf.Id);
    }

    public void UpdateMatch(int matchId, Match updatedMatch)
    {
        var matchFromEf = _mapper.Map<DAL.Entities.Match>(updatedMatch);
        matchFromEf.Id = matchId;
        _context.Attach(matchFromEf).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Matches.Any(p => p.Id == matchId))
                throw new EntityNotFoundException("Nem található a meccs!");
            else
                throw;
        }
    }
}
