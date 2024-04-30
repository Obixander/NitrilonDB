using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        //this is used to create an connection to the database
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //methods for the MemberController
        public int Add(Member member)
        {
            try
            {
                int newId = 0;

                if (member.Email == "")
                {
                    member.Email = null;
                }
                if (member.PhoneNumber == "")
                {
                    member.PhoneNumber = null;
                }

                string sql = $"INSERT INTO Members (Name,PhoneNumber,Email,Date,MembershipId) VALUES('{member.Name}', '{member.PhoneNumber}', '{member.Email}', '{DateTime.Now.ToString("yyyy-MM-dd")}' ,{member.Membership.MembershipId}); SELECT SCOPE_IDENTITY();";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4. Execute the insert Command and get the newly created id for the row
                newId = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return newId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Member> GetAllMembers()
        {
            try
            {
                //The idea is to get all the Memberships first and then when the Members have to be build then create the Members
                List<Member> members = new List<Member>();

                string sql = "SELECT * FROM Memberships; SELECT * FROM Members;";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4. Execute the select Command and get the newly created id for the row
                SqlDataReader reader = command.ExecuteReader();
                List<Membership> MembershipStatus = new List<Membership>();
                while (reader.Read())
                {
                    int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                    string MembershipName = reader["Name"].ToString();
                    string MembershipDescription = reader["Description"].ToString();

                    MembershipStatus.Add(new Membership(MembershipId, MembershipName, MembershipDescription));
                }
                if (reader.NextResult())
                { 
                    while (reader.Read())
                    {


                        int MemberId = Convert.ToInt32(reader["MemberId"]);
                        string Name = reader["Name"].ToString();
                        string Email = reader["Email"].ToString();
                        string PhoneNumber = reader["PhoneNumber"].ToString();
                        DateTime Date = (DateTime)reader["Date"];
                        int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                        Membership membership = null;
                        //This is a Active account
                        if (MembershipId == 1)
                        {
                            membership = MembershipStatus[0];
                        }
                        //this is a passive account
                        else if (MembershipId == 2)
                        {
                            membership = MembershipStatus[1];
                        }
                        //This is Deactived account
                        else if (MembershipId == -1)
                        {
                            membership = MembershipStatus[2];
                        }
                        else
                        {
                            throw new Exception("MemberId: "+ MemberId + " Has a MembershipId that is not valid");
                        }

                        Member member = new Member(MemberId, Name, PhoneNumber, Email, Date, membership);
                        members.Add(member);
                    }
                }

                connection.Close();

                return members;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Member GetMemberById(int id)
        {
            try
            {
                Member member = null;

                string sql = $"SELECT * FROM Membership; SELECT * FROM Members WHERE MemberId = {id};";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:

                connection.Open();

                //4. Execute the select Command and get the newly created id for the row
                SqlDataReader reader = command.ExecuteReader();
                List<Membership> MembershipStatus = new List<Membership>();
                while (reader.Read())
                {
                    int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                    string MembershipName = reader["Name"].ToString();
                    string MembershipDescription = reader["Description"].ToString();

                    MembershipStatus.Add(new Membership(MembershipId, MembershipName, MembershipDescription));
                }
                while (reader.Read())
                {
                    int MemberId = Convert.ToInt32(reader["MemberId"]);
                    string Name = reader["Name"].ToString();
                    string Email = reader["Email"].ToString();
                    string PhoneNumber = reader["PhoneNumber"].ToString();
                    DateTime Date = (DateTime)reader["Date"];
                    int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                    Membership membership = null;
                    if (MembershipId == 1)
                    {
                        membership = MembershipStatus[MembershipId];
                    }
                    else if (MembershipId == 2)
                    {
                        membership = MembershipStatus[MembershipId];
                    }

                    else
                    {
                        throw new Exception(MemberId + "Has a MembershipId that is not valid");
                    }

                    member = new Member(MemberId, Name, Email, PhoneNumber, Date, membership);

                }
                connection.Close();

                if (member == null)
                {
                    throw new Exception("Member not found");
                }
                return member;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, Member member)
        {
            try
            {
                string sql = $"UPDATE Members SET Name = '{member.Name}', PhoneNumber = '{member.PhoneNumber}', Email = '{member.Email}',Date = '{member.Date.ToString("yyyy-MM-dd")}', MembershipId = {member.Membership} WHERE MemberId = {id}";

                //1: make a sqlConnection Object:
                SqlConnection connection = new SqlConnection(connectionString);

                //2. Make a SqlCommand object:
                SqlCommand command = new SqlCommand(sql, connection);

                //3. Open the connection:
                connection.Open();

                //4. Execute the insert Command and get the newly created id for the row
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





        //Methods for EventRatingController

        //This method is used to get all Ratings by EventId and returns a count of the good, neutral and bad ratings
        public EventRatingData GetEventRatingDataBy(int eventId)
        {
            try
            {
                int BadRatingCount = 0;
                int NeutralRatingCount = 0;
                int GoodRatingCount = 0;
                EventRatingData eventRatingData = default;

                string sql = $"EXEC CountAllowedRatingsForEvent @EventiD = {eventId}";

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
                    BadRatingCount = Convert.ToInt32(reader["RatingId1Count"]);
                    NeutralRatingCount = Convert.ToInt32(reader["RatingId2Count"]);
                    GoodRatingCount = Convert.ToInt32(reader["RatingId3Count"]);
                    eventRatingData = new(BadRatingCount, NeutralRatingCount, GoodRatingCount);
                }
                //6. Close the connection:
                connection.Close();

                return eventRatingData;

            }
            catch (ArgumentException e)
            { //Change This later
                throw new Exception(e.Message);
            }
        }

        //Gets all Ratings from all events and returns it
        public List<EventRating> GetAllRatings()
        {

            try
            {
                List<EventRating> eventRatings = new();
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

                return eventRatings;

            }
            catch (ArgumentException e)
            { //Change This later
                throw new Exception(e.Message);
            }
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
                    //Change this from the default values to the stored procedure
                    int badRatingCount = 0;
                    int neutralRatingCount = 0;
                    int goodRatingCount = 0;


                    EventRatingData rating = new EventRatingData(badRatingCount, neutralRatingCount, goodRatingCount);
                    try
                    {
                        Event e = new Event(id, Date, name, attendees, description, rating);
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
                    //Change this from the default values to the stored procedure
                    int badRatingCount = 0;
                    int neutralRatingCount = 0;
                    int goodRatingCount = 0;


                    EventRatingData rating = new EventRatingData(badRatingCount, neutralRatingCount, goodRatingCount);
                    try
                    {
                        Event e = new Event(id, date, name, attendees, description, rating);
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
