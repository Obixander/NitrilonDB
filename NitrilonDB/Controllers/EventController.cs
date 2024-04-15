using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            try
            {
                Repository repo = new();
                return Ok(repo.Delete(id));
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Event eventToUpdate)
        {

            
            try
            {
                Repository repo = new();
                repo.UpdateEvent(eventToUpdate, id);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }

            return Ok(eventToUpdate);
        }

        //Gets all Events from the database
        [HttpGet]
        public IEnumerable<Event> GetAllRatings()
        {
            try
            {
                Repository repo = new();
                List<Event> events = repo.GetAllEvents();
                return events;
            }
            catch 
            {
                //change this later
                List<Event> events = new();
                return events;
            }
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
                return NotFound(e);
            }
        }


    }
}
