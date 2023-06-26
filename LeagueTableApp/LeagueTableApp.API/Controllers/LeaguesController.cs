using Microsoft.AspNetCore.Mvc;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Exceptions;
using NSwag.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeagueTableApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueService _leagueService;
        public LeaguesController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        // GET: api/<LeaguesController>
        /// <summary>
        /// Get all league
        /// </summary>
        /// <returns>Returns all leagues</returns>
        /// <response code="200">Listing successful</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<League>> Get()
        {
            return _leagueService.GetLeagues().ToList();
        }

        // GET api/<LeaguesController>/5
        /// <summary>
        /// Get a specific league with the given identifier
        /// </summary>
        /// <param name="id">League's identifier</param>
        /// <returns>Returns a specific league with the given identifier</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="404">No league with id</response>
        [HttpGet("{id}", Name = "GetLeague")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<League> Get(int id)
        {
            try
            {
                return _leagueService.GetLeague(id);
            }
            catch (EntityNotFoundException)
            {
                ProblemDetails details = new ProblemDetails
                {
                    Title = "Invalid ID",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"No league with ID {id}"
                };
                return NotFound(details);
            }

        }

        // POST api/<LeaguesController>
        /// <summary>
        /// Creates a new league.
        /// </summary>
        /// <param name="value">The league object to create.</param>
        /// <returns>The created league.</returns>
        /// <response code="201">Created successful</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<League> Post([FromBody] League value)
        {
            //Console.WriteLine(value);
            //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            var created = _leagueService.InsertLeague(value);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<LeaguesController>/5
        /// <summary>
        /// Updates a league by ID.
        /// </summary>
        /// <param name="id">The ID of the league to update.</param>
        /// <param name="value">The updated league object.</param>
        /// <returns>No content.</returns>
        /// <response code="204">No content</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(int id, [FromBody] League value)
        {
            _leagueService.UpdateLeague(id, value);
            return NoContent();
        }

        // DELETE api/<LeaguesController>/5
        /// <summary>
        /// Deletes a league by ID.
        /// </summary>
        /// <param name="id">The ID of the league to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">No content</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            _leagueService.DeleteLeague(id);
            return NoContent();
        }

        /// <summary>
        /// Retrieves the actual league table by ID.
        /// </summary>
        /// <param name="id">The ID of the league.</param>
        /// <returns>The actual league table.</returns>
        /// <response code="200">Listing successful</response>
        [HttpGet("GetActualLeagueTable/{id}", Name = "GetActualLeagueTable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PointsTable> GetActualLeagueTable(int id)
        {
            return _leagueService.GetActualLeagueTable(id);
        }
    }
}
