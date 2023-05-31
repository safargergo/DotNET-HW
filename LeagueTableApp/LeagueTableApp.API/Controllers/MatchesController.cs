using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using LeagueTableApp.BLL.Services;

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
        [HttpGet]
        public ActionResult<IEnumerable<Match>> Get()
        {
            return _matchService.GetMatches().ToList();
        }

        // GET api/<MatchesController>/5
        [HttpGet("{id}", Name = "GetMatch")]
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
        [HttpPost]
        public ActionResult<Match> Post([FromBody] Match match)
        {
            var created = _matchService.InsertMatch(match);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<MatchesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Match match)
        {
            _matchService.UpdateMatch(id, match);
            return NoContent();
        }

        // DELETE api/<MatchesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _matchService.DeleteMatch(id);
            return NoContent();
        }

        [HttpGet("MatchesOfLeague/{id}", Name = "GetMatchesOfLeague")]
        public ActionResult<IEnumerable<Match>> GetMatchesOfLeague(int id)
        {
            return _matchService.GetMatchesOfLeague(id).ToList();
        }

        [HttpGet("GenerateMatchesOfLeague/{id}", Name = "GenerateMatchesOfLeague")]
        public ActionResult<IEnumerable<Match>> GenerateMatchesOfLeague(int id)
        {
            return _matchService.GenerateMatchesOfLeague(id).ToList();
        }
    }
}
