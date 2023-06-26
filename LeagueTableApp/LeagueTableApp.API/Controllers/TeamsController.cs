using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using LeagueTableApp.BLL.Services;
using System.Net;

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
        /// <summary>
        /// Retrieves all teams.
        /// </summary>
        /// <returns>A list of all teams.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Team>> Get()
        {
            return _teamService.GetTeams().ToList();
        }

        // GET api/<TeamsController>/5
        /// <summary>
        /// Retrieves a team by ID.
        /// </summary>
        /// <param name="id">The ID of the team.</param>
        /// <returns>The team.</returns>
        [HttpGet("{id}", Name = "GetTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="team">The team to create.</param>
        /// <returns>The created team.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<Team> Post([FromBody] Team team)
        {
            try
            {
                var created = _teamService.InsertTeam(team);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            } catch (AlreadyUsedNameAtInsertException)
            {
                return UnprocessableEntity("This team name is already taken!");
            }
            
        }

        // PUT api/<TeamsController>/5
        /// <summary>
        /// Updates a team by ID.
        /// </summary>
        /// <param name="id">The ID of the team to update.</param>
        /// <param name="team">The updated team object.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(int id, [FromBody] Team team)
        {
            _teamService.UpdateTeam(id, team);
            return NoContent();
        }

        // DELETE api/<TeamsController>/5
        /// <summary>
        /// Deletes a team by ID.
        /// </summary>
        /// <param name="id">The ID of the team to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            _teamService.DeleteTeam(id);
            return NoContent();
        }

        // GET api/<TeamsController>/5
        /// <summary>
        /// Retrieves the played matches of a team by the league's ID.
        /// </summary>
        /// <param name="id">The ID of the team.</param>
        /// <returns>The list of played matches.</returns>
        [HttpGet("PlayedMatches/{id}", Name = "GetPlayedMatchesOfTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Match>> GetPlayedMatchesOfTeam(int id)
        {
            return _teamService.ListPlayedMatchesOfTeam(id).ToList();
        }

        // GET api/<TeamsController>/5
        /// <summary>
        /// Retrieves the teams of a league by the league's ID.
        /// </summary>
        /// <param name="id">The ID of the league.</param>
        /// <returns>The list of teams in the league.</returns>
        [HttpGet("TeamsOfLeague/{id}", Name = "GetTeamsOfLeague")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Team>> GetTeamsOfLeague(int id)
        {
            return _teamService.GetTeamsOfLeague(id).ToList();
        }
    }
}
