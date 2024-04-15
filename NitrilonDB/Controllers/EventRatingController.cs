using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace NitrilonDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRatingController : ControllerBase
    {
        //i Dont think adding a "put" makes any amount of sense 
        //TODO: PUT(ID)
        [HttpGet]
        public IEnumerable<EventRating> GetAll()
        {
            Repository repo = new();
            List<EventRating> EventRatings = repo.GetAllRatings();
            return EventRatings;
        }

        [HttpGet("{id}")]
        public ActionResult<EventRating> Get(int id)
        {
            Repository repo = new();
            List<EventRating> Ratings = repo.GetAllRatings();
            List<EventRating> ReturnList = new();
            foreach (EventRating ev in Ratings)
            {
                if (ev.EventId == id)
                {
                    ReturnList.Add(ev);
                }
            }
            return Ok(ReturnList);
        }

        [HttpPost]
        public IActionResult Add(EventRating newRating)
        {
            try
            {
                Repository repo = new();
                int CreatedId = repo.SaveRating(newRating);
                return Ok(CreatedId);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


    }
}
