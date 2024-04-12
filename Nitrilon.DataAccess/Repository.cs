using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

         //Methods for EventController
        public List<Event> GetAllEvents()
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
            try
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EventId"]);
                    DateTime date = Convert.ToDateTime(reader["Date"]);
                    string name = Convert.ToString(reader["Name"]);
                    int attendees = Convert.ToInt32(reader["Attendees"]);
                    string description = Convert.ToString(reader["Description"]);

                    Event e = new()
                    {
                        Id = id,
                        Date = date,
                        Name = name,
                        Attendees = attendees,
                        Description = description
                    };

                    events.Add(e);
                }
            }
            catch
            {
                return events;
            }

            //6. Close the connection:
            connection.Close();

            return events;
        }

        public int Save(Event newEvent)
        {
            int newId = 0;

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
            catch //TODO: Add Better error Handling
            {
                return "Process Failed";
            }
        }

        public string UpdateEvent(Event Event, int id)
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


    }
}
