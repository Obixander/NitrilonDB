using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class EventRatingData
    {
        private int bad;
        private int neutral;
        private int good;

        public EventRatingData(int bad, int neutral, int good)
        {
            Bad = bad;
            Neutral = neutral;
            Good = good;
        }

        public int Bad { get => bad; set => bad = value; }
        public int Neutral { get => neutral; set => neutral = value; }
        public int Good { get => good; set => good = value; }
    }
}
