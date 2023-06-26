using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Exceptions;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        var theMatch = _context.Matches
            //.ProjectTo<Match>(_mapper.ConfigurationProvider)
            .SingleOrDefault(t => t.Id == matchId);
        if (theMatch == null) return;

        //var matchFromEf = _mapper.Map<DAL.Entities.Match>(theMatch);
        //matchFromEf.IsDeleted = true;
        theMatch.IsDeleted = true;
        _context.Attach(theMatch).State = EntityState.Modified;
        //_context.Matches.Remove(new DAL.Entities.Match() { Id = matchId });
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
        //newMatch.RowVersion = BitConverter.GetBytes(0x0000000000000000);//(0x0000000000002711);
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

    public IEnumerable<Match> GetMatchesOfLeague(int leagueId)
    {
        var matches = _context.Matches
            .Where(m => leagueId == m.LeagueId)
            .ProjectTo<Match>(_mapper.ConfigurationProvider).AsEnumerable();
        return matches;
    }

    public IEnumerable<Match> GenerateMatchesOfLeague(int leagueId)
    {
        var matches = GetMatchesOfLeague(leagueId);
        if (matches.Count() > 0)
        {
            return matches;
        }
        else
        {
            var allTeams = _context.Teams
            .Where(t => t.LeagueId == leagueId)
            .ProjectTo<Team>(_mapper.ConfigurationProvider).AsEnumerable().ToList();
            //IEnumerable<Match> newMatches = new List<Match>();
            var teamCount = allTeams.Count();//_context.Teams.Where(t => t.LeagueId == leagueId).Count();
            if (teamCount < 2)
            {
                throw new MatchCreationException("Nem lehet 2-nél kevesebb csapattal meccseket generálni!");
            }
            var homeTeams = new List<Team>();
            foreach (var team in allTeams)
            {
                homeTeams.Add(team);
            }
            foreach (var homeTeam in homeTeams)
            {
                var otherTeams = new List<Team>();
                foreach (var team in allTeams)
                {
                    otherTeams.Add(team);
                }
                otherTeams.Remove(homeTeam);
                foreach (var otherTeam in otherTeams)
                {
                    Match newMatch = new();
                    newMatch.LeagueId = leagueId;
                    newMatch.HomeTeamId = homeTeam.Id;
                    newMatch.ForeignTeamId = otherTeam.Id;
                    //newMatch.HomeTeam = homeTeam;
                    //newMatch.ForeignTeam = otherTeam;
                    newMatch.HomeTeamScore = 0;
                    newMatch.ForeignTeamScore = 0;
                    newMatch.IsEnded = false;
                    //newMatches.Append(newMatch);
                    InsertMatch(newMatch);
                }
            }
            return GetMatchesOfLeague(leagueId);
        }
        
    }
}
