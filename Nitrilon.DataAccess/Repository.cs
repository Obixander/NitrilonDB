using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        //Methods for EventRatingController
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

        public string GetActiveOrFutureEvents()
        {
            List<Event> events = new List<Event>();
            try
            {
                DateTime date = DateTime.UtcNow;
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

                    try
                    {
                        Event e = new Event(id, date, name, attendees, description);
                        events.Add(e);
                    }
                    catch (ArgumentException Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
             
                }

                //6. Close the connection:
                connection.Close();

                //Change this later please
                string ReturnValues = "";
                for (int i = 0; i < events.Count; i++)
                {
                    ReturnValues += events[i].Id.ToString() + ": ";
                    ReturnValues += events[i].Name;
                    ReturnValues += " (" + events[i].Date.ToString("yyyy-MM-dd");
                    ReturnValues += ") | ";
                }

                return ReturnValues;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }


        }


        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();
            try
            {
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
                    try
                    {
                        Event e = new Event(id, date, name, attendees, description);
                        events.Add(e);
                    }
                    catch (ArgumentException Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
                }
                //6. Close the connection:
                connection.Close();
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }


            return events;
        }

        public int Save(Event newEvent)
        {
            int newId = 0;
            try
            {


                //TODO: handle attendees when the event is not yet over.
                //dont forget to format a date as 'yyyy-MM-dd'
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
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }

            return newId;
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
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            return "Success";
        }


    }
}
