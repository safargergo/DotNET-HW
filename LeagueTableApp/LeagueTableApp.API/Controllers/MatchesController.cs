using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using LeagueTableApp.BLL.Services;
using System.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeagueTableApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {

        private readonly IMatchService _matchService;
        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // GET: api/<MatchesController>
        /// <summary>
        /// Retrieves all matches.
        /// </summary>
        /// <returns>The list of matches.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Match>> Get()
        {
            return _matchService.GetMatches().ToList();
        }

        // GET api/<MatchesController>/5
        /// <summary>
        /// Retrieves a match by ID.
        /// </summary>
        /// <param name="id">The ID of the match.</param>
        /// <returns>The match.</returns>
        [HttpGet("{id}", Name = "GetMatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Match> Get(int id)
        {
            try
            {
                return _matchService.GetMatch(id);
            }
            catch (EntityNotFoundException)
            {
                ProblemDetails details = new ProblemDetails
                {
                    Title = "Invalid ID",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"No match with ID {id}"
                };
                return NotFound(details);
            }
        }

        // POST api/<MatchesController>
        /// <summary>
        /// Creates a new match.
        /// </summary>
        /// <param name="match">The match to create.</param>
        /// <returns>The created match.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Match> Post([FromBody] Match match)
        {
            var created = _matchService.InsertMatch(match);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<MatchesController>/5
        /// <summary>
        /// Updates a match by ID.
        /// </summary>
        /// <param name="id">The ID of the match.</param>
        /// <param name="match">The updated match.</param>
        /// <returns>No content.</returns>
        /// <returns>Data has been modified by someone else.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Put(int id, [FromBody] Match match)
        {
            try
            {
                _matchService.UpdateMatch(id, match);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                ProblemDetails details = new ProblemDetails
                {
                    Title = "Data has been modified by someone else.",
                    Status = StatusCodes.Status409Conflict,
                };
                return Conflict(details);
            }
        }

        // DELETE api/<MatchesController>/5
        /// <summary>
        /// Deletes a match by ID.
        /// </summary>
        /// <param name="id">The ID of the match.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            _matchService.DeleteMatch(id);
            return NoContent();
        }

        /// <summary>
        /// Retrieves matches of a specific league.
        /// </summary>
        /// <param name="id">The ID of the league.</param>
        /// <returns>The list of matches.</returns>
        [HttpGet("MatchesOfLeague/{id}", Name = "GetMatchesOfLeague")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Match>> GetMatchesOfLeague(int id)
        {
            return _matchService.GetMatchesOfLeague(id).ToList();
        }

        /// <summary>
        /// Generates matches for a specific league.
        /// </summary>
        /// <param name="id">The ID of the league.</param>
        /// <returns>The list of generated matches.</returns>
        [HttpGet("GenerateMatchesOfLeague/{id}", Name = "GenerateMatchesOfLeague")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Match>> GenerateMatchesOfLeague(int id)
        {
            try
            {
                return _matchService.GenerateMatchesOfLeague(id).ToList();
            }
            catch (MatchCreationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
