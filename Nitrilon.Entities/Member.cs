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
        private Membership membershipId;

        public Member(int memberId, string name, string phoneNumber, string email, DateTime date, Membership membershipId)
        {
            MemberId = memberId;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Date = date;
            MembershipId = membershipId;
        }

        public int MemberId { get => memberId; set => memberId = value; }
        public string Name { get => name; set => name = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Date { get => date; set => date = value; }
        public Membership MembershipId { get => membershipId; set => membershipId = value; }
    }
}
