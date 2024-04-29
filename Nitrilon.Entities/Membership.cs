using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Membership
    {
        private int membershipId;
        private string name;
        private string description;

        public Membership(int membershipId, string name, string description)
        {
            MembershipId = membershipId;
            Name = name;
            Description = description;
        }

        public int MembershipId { get => membershipId; set => membershipId = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
    }
}
