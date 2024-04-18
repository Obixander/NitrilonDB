namespace Nitrilon.Entities
{
    public class Event
    {   
        private int id;
        private DateTime date;
        private string name;
        private int attendees;
        private string description;
        private List<Rating> ratings;

        public Event(int id, DateTime date, string name, int attendees, string description, List<Rating> ratings)
        {
            Id = id;
            Date = date;
            Name = name;
            Attendees = attendees;
            Description = description;
            //check if ratings is null and throws exception if true
            this.ratings = ratings ?? throw new ArgumentNullException(nameof(ratings));

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
                if (value < -1 )
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
    
        public void Add(Rating rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating));
            }
            ratings.Add(rating);
        }

        public double GetRatingAverage()
        {
            if (ratings == null)
            {
                throw new ArgumentNullException();
            }
            double sum = 0;
            foreach (Rating rating in ratings)
            {
                sum += rating.RatingValue;
            }
            sum = sum / ratings.Count();
            return sum;
        }
    }
}
