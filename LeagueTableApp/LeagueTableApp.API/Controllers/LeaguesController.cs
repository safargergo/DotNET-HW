using Microsoft.AspNetCore.Mvc;
using LeagueTableApp.BLL.Interfaces;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Exceptions;

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
        [HttpGet]
        public ActionResult<IEnumerable<League>> Get()
        {
            return _leagueService.GetLeagues().ToList();
        }

        // GET api/<LeaguesController>/5
        [HttpGet("{id}", Name = "GetLeague")]
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
        [HttpPost]
        public ActionResult<League> Post([FromBody] League value)
        {
            Console.WriteLine(value);
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            var created = _leagueService.InsertLeague(value);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<LeaguesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] League value)
        {
            _leagueService.UpdateLeague(id, value);
            return NoContent();
        }

        // DELETE api/<LeaguesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _leagueService.DeleteLeague(id);
            return NoContent();
        }

        [HttpGet("GetActualLeagueTable/{id}", Name = "GetActualLeagueTable")]
        public ActionResult<PointsTable> GetActualLeagueTable(int id)
        {
            return _leagueService.GetActualLeagueTable(id);
        }
    }
}
