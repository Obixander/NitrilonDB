namespace Nitrilon.Entities
{
    public class Event
    {   
        private int id;
        private DateTime date;
        private string name;
        private int attendees;
        private string description;

        public int Id 
        { 
            get
            {
                return id;
            } 
            set
            {
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
                if (String.IsNullOrWhiteSpace(value.ToString()))
                {
                    throw new ArgumentException("Date cannot include a Space");
                }
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
    }
}
