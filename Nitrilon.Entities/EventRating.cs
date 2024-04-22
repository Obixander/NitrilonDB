using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class EventRating
    {
        private int eventRatingId;
        private int eventId;
        private int ratingId;
        public int EventRatingId
        {
            get
            {
                return eventRatingId;
            }
            set
            {
                if (eventRatingId != value)
                {
                    eventRatingId = value;
                }
            }
        }
        public int EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The EventId cannot be below 1");
                }
                eventId = value;
            }
        }
        public int RatingId
        {
            get
            {
                return ratingId;
            }
            set
            {
                if (value <= 0 || value > 3)
                {
                    throw new ArgumentException("RatingId Cannot be below 1 or above 3");
                }
                ratingId = value;
            }
        }

    }
}
