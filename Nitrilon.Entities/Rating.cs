using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Rating
    {
        private int id;
        private int ratingValue;
        private string description;

        public Rating(int id, int ratingValue, string description)
        {
            Id = id;
            RatingValue = ratingValue;
            Description = description;
        }

        public int Id { get => id; set => id = value; }
        public int RatingValue { get => ratingValue; set => ratingValue = value; }
        public string Description { get => description; set => description = value; }
    }
}
