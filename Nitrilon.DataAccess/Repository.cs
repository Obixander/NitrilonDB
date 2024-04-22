using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        //this is used to create an connection to the database
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        //Methods for EventRatingController

        //Gets all Ratings from all events and returns it
        public List<EventRating> GetAllRatings()
        {

            List<EventRating> eventRatings = new();
            try
            {
                string sql = $"SELECT * FROM EventRatings";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4.Execute query:
                SqlDataReader reader = command.ExecuteReader();

                //5 Rerieve data from the data reader: 

                while (reader.Read())
                {

                    int EventRatingId = Convert.ToInt32(reader["EventRatingId"]);
                    int EventId = Convert.ToInt32(reader["EventId"]);
                    int RatingId = Convert.ToInt32(reader["RatingId"]);

                    eventRatings.Add(new()
                    {
                        EventRatingId = EventRatingId,
                        EventId = EventId,
                        RatingId = RatingId
                    });

                }
                //6. Close the connection:
                connection.Close();
            }
            catch (ArgumentException e)
            { //Change This later
                throw new Exception(e.Message);
            }
            return eventRatings;
        }

        //this is method is used to add a rating to the database
        public int SaveRating(EventRating newRating)
        {
            int newId = 0;
            if (newRating.EventId <= 0)
            {
                return -1;
            }
            //TODO: handle attendees when the event is not yet over.
            //dont forget to format a date as 'yyyy-MM-dd'
            try
            {

                string sql = $"INSERT INTO EventRatings (EventId, RatingID) VALUES({newRating.EventId}, {newRating.RatingId}); SELECT SCOPE_IDENTITY();";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4. Execute the insert Command and get the newly created id for the row
                //command.ExecuteNonQuery();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    newId = (int)sqlDataReader.GetDecimal(0);
                }

                //5. Close the Connection when it is not needed anymore.
                connection.Close();

                return newId;
            }
            catch (ArgumentException e)
            {
                //Change This later
                throw new Exception(e.Message);
            }

        }


        //Methods for EventController
        //This Method is used to get all Events in the "future" - 3 days
        //returns list<Event>
        public List<Event> GetActiveOrFutureEvents()
        {
            List<Event> events = new List<Event>();
            try
            {
                //This is a temp solution until a frontend way to select the date is made
                DateTime date = DateTime.Now.AddDays(-3); //Minus a couple days to insure that even if the Event has begun they can still set up the Rating system
                string sql = $"SELECT * FROM Events WHERE Date >= '{date.ToString("yyyy-MM-dd")}'";
                
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4.Execute query:
                SqlDataReader reader = command.ExecuteReader();

                //5 Rerieve data from the data reader: 
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EventId"]);
                    DateTime Date = Convert.ToDateTime(reader["Date"]);
                    string name = Convert.ToString(reader["Name"]);
                    int attendees = Convert.ToInt32(reader["Attendees"]);
                    string description = Convert.ToString(reader["Description"]);
                    //add this to the event e later
                    List<Rating> ratingList = new List<Rating>();
                    try
                    {
                        Event e = new Event(id, Date, name, attendees, description, ratingList);
                        events.Add(e);
                    }
                    catch (ArgumentException Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
             
                }

                //6. Close the connection:
                connection.Close();

                return events;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }


        }

        //This Method is used to get all Events from the Database and
        //Return list<Event>
        public List<Event> GetAllEvents()
        {
            try
            {
                List<Event> events = new List<Event>();
                string sql = $"SELECT * FROM Events";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4.Execute query:
                SqlDataReader reader = command.ExecuteReader();

                //5 Rerieve data from the data reader: 

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EventId"]);
                    DateTime date = Convert.ToDateTime(reader["Date"]);
                    string name = Convert.ToString(reader["Name"]);
                    int attendees = Convert.ToInt32(reader["Attendees"]);
                    string description = Convert.ToString(reader["Description"]);
                    List<Rating> ratingList = new List<Rating>();
                    try                    
                    {
                        Event e = new Event(id, date, name, attendees, description, ratingList);
                        events.Add(e);
                    }
                    catch (ArgumentException Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
                }
                //6. Close the connection:
                connection.Close();

                return events;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }

        //This method is used to Add a new Event to the Database
        //Returns int Id : Id of the Created Event in Database
        public int Save(Event newEvent)
        {
            try
            {
                int newId = 0;
                //TODO: handle attendees when the event is not yet over.
                string sql = $"INSERT INTO events (Date, Name,Attendees, Description) VALUES('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}',{newEvent.Attendees}, '{newEvent.Description}'); SELECT SCOPE_IDENTITY();";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4. Execute the insert Command and get the newly created id for the row
                //command.ExecuteNonQuery();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    newId = (int)sqlDataReader.GetDecimal(0);
                }

                //5. Close the Connection when it is not needed anymore.
                connection.Close();
                
                return newId;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

        }


        //This Method is used to Delete an Event from the Database based on "EventId"
        public string Delete(int EventId)
        {
            try
            {
                string sql = $"DELETE FROM Events WHERE EventId = {EventId}";
                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:
                connection.Open();

                //4. Execute the delete Command
                command.ExecuteNonQuery();

                //5. Close the Connection when it is not needed anymore.
                connection.Close();

                return "Deleted";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }
        //this method is used to update an Event in the database
        public string UpdateEvent(Event Event, int id)
        {
            try
            {

                string sql = $"UPDATE Events " +
                               $"SET Date = '{Event.Date.ToString("yyyy-MM-dd")}', " +
                               $"Name = '{Event.Name}', " +
                               $"Attendees = {Event.Attendees}, " +
                               $"Description = '{Event.Description}'" +
                               $" WHERE EventId = {id}";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:
                connection.Open();

                //4. Execute the delete Command
                command.ExecuteNonQuery();

                //5. Close the Connection when it is not needed anymore.
                connection.Close();
                
                return "Success";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

    }
}
