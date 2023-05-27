using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using LeagueTableApp.BLL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeagueTableApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/<TeamsController>
        [HttpGet]
        public ActionResult<IEnumerable<Team>> Get()
        {
            return _teamService.GetTeams().ToList();
        }

        // GET api/<TeamsController>/5
        [HttpGet("{id}", Name = "GetTeam")]
        public ActionResult<Team> Get(int id)
        {
            try
            {
                return _teamService.GetTeam(id);
            }
            catch (EntityNotFoundException)
            {
                ProblemDetails details = new ProblemDetails
                {
                    Title = "Invalid ID",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"No team with ID {id}"
                };
                return NotFound(details);
            }
        }

        // POST api/<TeamsController>
        [HttpPost]
        public ActionResult<Team> Post([FromBody] Team team)
        {
            var created = _teamService.InsertTeam(team);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Team team)
        {
            _teamService.UpdateTeam(id, team);
            return NoContent();
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _teamService.DeleteTeam(id);
            return NoContent();
        }
    }
}
