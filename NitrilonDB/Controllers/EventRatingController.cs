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
        //[HttpGet]
        //public IEnumerable<EventRating> GetAll()
        //{
        //    Repository repo = new();
        //    List<EventRating> EventRatings = repo.GetAllRatings();
        //    return EventRatings;
        //}

        [HttpGet("{id}")]
        public ActionResult<EventRating> Get(int id)
        {
            EventRepository repo = new();
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
                EventRepository repo = new();
                int CreatedId = repo.SaveRating(newRating);
                return Ok(CreatedId);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
        //This method is used to get all Ratings by EventId and returns a count of the good, neutral and bad ratings
        [HttpGet]
        public ActionResult<EventRatingData> GetEventRatingDataFor(int eventId)
        {
            EventRepository repository = new();
            EventRatingData eventRatingData = repository.GetEventRatingDataBy(eventId);
            return Ok(eventRatingData);
        }


    }
}
