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

internal class TeamService : ITeamService
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
        _context.Teams.Remove(new DAL.Entities.Team(null!) { Id = teamId });
        _context.SaveChanges();
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
    }
}
