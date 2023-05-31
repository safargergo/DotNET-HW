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

public class TeamService : ITeamService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public TeamService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void DeleteTeam(int teamId)
    {
        _context.Teams.Remove(new DAL.Entities.Team(null!, null!, null!, null!) { Id = teamId });
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Teams.Any(p => p.Id == teamId))
                throw new EntityNotFoundException("Nem található a csapat!");
            else
                throw;
        }
    }

    public Team GetTeam(int teamId)
    {
        return _context.Teams
            .ProjectTo<Team>(_mapper.ConfigurationProvider)
            .SingleOrDefault(t => t.Id == teamId) ?? throw new EntityNotFoundException("Nem található ilyen csapat!");
    }   

    public IEnumerable<Team> GetTeams()
    {
        return _context.Teams.ProjectTo<Team>(_mapper.ConfigurationProvider).AsEnumerable();
    }

    public Team InsertTeam(Team newTeam)
    {
        var teamFromEf = _mapper.Map<DAL.Entities.Team>(newTeam);
        _context.Teams.Add(teamFromEf);
        _context.SaveChanges();
        return GetTeam(teamFromEf.Id);
    }

    public void UpdateTeam(int teamId, Team updatedTeam)
    {
        var teamFromEf = _mapper.Map<DAL.Entities.Team>(updatedTeam);
        teamFromEf.Id = teamId;
        _context.Attach(teamFromEf).State = EntityState.Modified;
        _context.SaveChanges();
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Teams.Any(p => p.Id == teamId))
                throw new EntityNotFoundException("Nem található a csapat!");
            else
                throw;
        }
    }

    public IEnumerable<Match> ListPlayedMatchesOfTeam(int teamId)
    {
        /*var matches = _context.Teams
            .Where(t => t.Id == teamId)
            .Include(t => t.League)
                .ThenInclude(l => l.Matches)
            .Select(t => t.League.Matches);
        var teamsMatches = matches
            .ProjectTo<Match>(_mapper.ConfigurationProvider).AsEnumerable()
            .Where(m => m.IsEnded==true && (teamId == m.HomeTeamId || teamId == m.ForeignTeamId));*/
        var teamsMatches = _context.Matches
            .Where(m => m.IsEnded == true && (teamId == m.HomeTeamId || teamId == m.ForeignTeamId))
            .ProjectTo<Match>(_mapper.ConfigurationProvider).AsEnumerable();
        return teamsMatches;
        /*if (teamsMatches != null && teamsMatches.Any())
        {
            return teamsMatches;
        }
        else
        {
            return Enumerable.Empty<Match>();
        }*/
    }

    public IEnumerable<Team> GetTeamsOfLeague(int leagueId)
    {
        var teams = _context.Teams
            .Where(t => leagueId == t.LeagueId)
            .ProjectTo<Team>(_mapper.ConfigurationProvider).AsEnumerable();
        return teams;
    }
}
