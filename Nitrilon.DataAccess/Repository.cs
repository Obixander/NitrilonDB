using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

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
            while(reader.Read())
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

            //6. Close the connection:
            connection.Close();

            return events;
        }

        public int Save(Event newEvent)
        {
            //TODO: handle attendees when the event is not yet over.
            //dont forget to format a date as 'yyyy-MM-dd'
            string sql = $"INSERT INTO events (Date, Name,Attendees, Description) VALUES('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}',{newEvent.Attendees}, '{newEvent.Description}')";
            //1: make a sqlConnection Object:
            SqlConnection connection = new SqlConnection(connectionString);

            //2. Make a SqlCommand object:
            SqlCommand command = new SqlCommand(sql, connection);

            //3. Open the connection:
            
                connection.Open();
                //TODO: Fiure out how to get the new id created in the table.
                //4. Execute4 the insert Command
                command.ExecuteNonQuery();

                //5. Close the Connection when it is not needed anymore.
                connection.Close();
            
            return -1;
        }
    }
}
