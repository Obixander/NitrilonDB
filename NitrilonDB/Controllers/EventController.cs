using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace NitrilonDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Repository repo = new();
            return Ok(repo.Delete(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Event eventToUpdate)
        {

            Repository repo = new();
            try
            {
                repo.UpdateEvent(eventToUpdate, id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(eventToUpdate);
        }

        //Gets all Events from the database
        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            Repository repo = new();
            List<Event> events = repo.GetAllEvents();
            return events;
        }

        [HttpGet("{id}")]
        public ActionResult<Event> Get(int id)
        {
            Repository repo = new();
            List<Event> e = repo.GetAllEvents();
            foreach (Event ev in e)
            {
                if (ev.Id == id)
                {
                    return Ok(ev);
                }
            }            
                return NotFound($"The requested event the id {id} was not found");
         
        }
        [HttpPost]
        public IActionResult Add(Event newEvent)
        {
            try
            {
                Repository repo = new();
                int CreatedId = repo.Save(newEvent);
                return Ok(CreatedId);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


    }
}
