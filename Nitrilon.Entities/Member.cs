using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Member
    {
        private string name;
        private string phoneNumber;
        private string email;
        private DateTime date;
        private int MembershipId;

        public Member(string name, string phoneNumber, string email, DateTime date, int membershipId1)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Date = date;
            MembershipId1 = membershipId1;
        }

        public string Name { get => name; set => name = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Date { get => date; set => date = value; }
        public int MembershipId1 { get => MembershipId; set => MembershipId = value; }
    }
}
