
using System.Text.Json.Serialization;

namespace Nitrilon.Entities
{
    public class Event
    {
        


        #region Fields
        private int id;
        private DateTime date;
        private string name;
        private int attendees;
        private string description;
        private List<Rating> ratings;
        #endregion


        [JsonConstructor]
        public Event() { }

        public Event(int id, DateTime date, string name, int attendees, string description, List<Rating> Ratings)
        {
            Id = id;
            Date = date;
            Name = name;
            Attendees = attendees;
            Description = description;
            //check if ratings is null and throws exception if true
            ratings = Ratings;
        }


        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id cannot be or be below 0");
                }
                id = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }
        public int Attendees
        {
            get
            {
                return attendees;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentException("Attendees cannot be negative");
                }
                attendees = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        //this method is used for adding new ratings to the list<Rating> ratings
        public void Add(Rating rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating));
            }
            ratings.Add(rating);
        }


        //This method calculates the average rating of the event
        public double GetRatingAverage()
        {
            //throw an exception if null
            if (ratings == null)
            {
                throw new ArgumentNullException();
            }
            //check if the list is empty as to not divide by 0
            if (ratings.Count > 0)
            {
                double sum = 0;
                double average = 0.0;
                foreach (Rating rating in ratings)
                {
                    sum += rating.RatingValue;
                }
                average = (double)sum / (double)ratings.Count();
                return average;
            }
            else
            {
                return -1.0;
            }
        }
    }
}
