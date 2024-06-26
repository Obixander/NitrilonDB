﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nitrilon.DataAccess;
using Nitrilon.Entities;
using System.Collections.Generic;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NitrilonDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {  
        [HttpGet]
        [Route("GetActiveOrFutureEvents")]
        public IActionResult GetActiveOrFutureEvents()
        {
            try
            {
                EventRepository repo = new();
                List<Event> events = repo.GetActiveOrFutureEvents();
                return Ok(events);
            }
            catch (ArgumentException e) 
            {
                Console.WriteLine(e.Message);
                return NotFound(e);
            }
            
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EventRepository repo = new();
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
                EventRepository repo = new();
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
        public ActionResult<Event> GetAllRatings()
        {
            try
            {
                EventRepository repo = new();
                List<Event> events = repo.GetAllEvents();
                return Ok(events);
            }
            catch (ArgumentException e)
            {
                
                return NotFound(e);
            }
        }
        //why is this here?
        //[HttpGet("{id}")]
        //public ActionResult<Event> Get(int id)
        //{
        //    Repository repo = new();
        //    List<Event> e = repo.GetAllEvents();
        //    foreach (Event ev in e)
        //    {
        //        if (ev.Id == id)
        //        {
        //            return Ok(ev);
        //        }
        //    }            
        //        return NotFound($"The requested event the id {id} was not found");
         
        //}

        [HttpPost]
        public IActionResult Add(Event newEvent)
        {
            try
            {
                EventRepository repo = new();

                //Change this from the default values to the stored procedure
                int badRatingCount = 0;
                int neutralRatingCount = 0;
                int goodRatingCount = 0;


                EventRatingData rating = new EventRatingData(badRatingCount, neutralRatingCount, goodRatingCount);

                int id = newEvent.Id;
                DateTime date = newEvent.Date;
                string name = newEvent.Name;
                int attendees = newEvent.Attendees;
                string description = newEvent.Description;
                Event e = new Event(id, date, name, attendees, description, rating);
                Console.WriteLine("test");
                int CreatedId = repo.Save(e);
                return Ok(CreatedId);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }




    }
}
