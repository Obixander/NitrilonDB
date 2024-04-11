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
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Event eventToUpdate)
        {
            
            return Ok(eventToUpdate);
        }

        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            List<Event> events = new()
            {
                new() {Id=1}, new() {Id=2}
            };
            return events;
        }

        [HttpGet("{id}")]        
        public ActionResult<Event> Get(int id)
        {
            Event e = null;
            if (id == 3)
            {
                e = new Event { Id = 3 };
            }
            else
            {
                return NotFound($"The requested event tih id {id} was not found");
            }
            return e;
        }
        [HttpPost]
        public IActionResult Add(Event newEvent)
        {
            try
            {
                Repository repo = new();
                int CreatedId = repo.Save(newEvent);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        } 


    }
}
