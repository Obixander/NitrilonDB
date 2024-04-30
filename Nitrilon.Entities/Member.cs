using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Member
    {
        private int memberId;
        private string name;
        private string phoneNumber;
        private string email;
        private DateTime date;
        private Membership membership;

        public Member(int memberId, string name, string phoneNumber, string email, DateTime date, Membership membership)
        {
            MemberId = memberId;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Date = date;
            Membership = membership;
        }

        public int MemberId { 
            get => memberId;
            set {
                if (value != memberId)
                {
                    memberId = value;                    
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
        public string PhoneNumber { 
            get => phoneNumber;
            set {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                }
            }
        }
        public string Email { 
            get => email;
            set {
                if (value != email)
                {
                    email = value;
                }
            }
        }
        public DateTime Date { 
            get => date;
            set  {
                if (value != date)
                { date = value; }
            }
        }
        public Membership Membership { 
            get => membership;
            set {
                if (value != membership)
                membership = value;
            }
        }
    }
}
