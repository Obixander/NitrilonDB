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

        public int MembershipId {
            get => membershipId;
            set {
                if (value < -1 || value == 0)
                {
                    throw new ArgumentException("MembershipId cannot be below -1 or be 0");
                }
                if (value != membershipId)
                { 
                    membershipId = value;
                }
            }
        }
        public string Name { 
            get => name;
            set {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name cannot be empty");
                }
                if (value != name)
                {
                    name = value;
                }
            }
        }
        public string Description { 
            get => description;
            set { 
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Description cannot be empty");
                }
                if (value != description)
                { 
                    description = value;
                }
            }
        }
    }
}
