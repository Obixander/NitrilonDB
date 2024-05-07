using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.DataAccess
{
    public class MemberRepository : Repository
    {

        public MemberRepository()
        {
            
        }

        //methods for the MemberController
        public int Add(Member member)
        {
            try
            {
                int newId = 0;
               
                //if (member.Email == "")
                //{
                //    member.Email = null;
                //}
                //if (member.PhoneNumber == "")
                //{
                //    member.PhoneNumber = null;
                //}

                string sql = $"INSERT INTO Members (Name,PhoneNumber,Email,Date,MembershipId) VALUES('{member.Name}', '{member.PhoneNumber}', '{member.Email}', '{DateTime.Now.ToString("yyyy-MM-dd")}' ,{member.Membership.MembershipId}); SELECT SCOPE_IDENTITY();";

                SqlDataReader reader = Execute(sql);

                if (reader.Read())
                {
                    newId = Convert.ToInt32(reader[0]);
                }
                CloseConnection();

                return newId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Member> GetAllMembers()
        {
            try
            {
                //The idea is to get all the Memberships first and then when the Members have to be build then create the Members
                List<Member> members = new List<Member>();

                string sql = "SELECT * FROM Memberships; SELECT * FROM Members;";

                //4. Execute the select Command and get the newly created id for the row
                SqlDataReader reader = Execute(sql);
                List<Membership> MembershipStatus = new List<Membership>();
                while (reader.Read())
                {
                    int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                    string MembershipName = reader["Name"].ToString();
                    string MembershipDescription = reader["Description"].ToString();

                    MembershipStatus.Add(new Membership(MembershipId, MembershipName, MembershipDescription));
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                        //this prevents it from creating members that are deactived
                        //TODO: Find a better way then hard coding "4"!
                        if (MembershipId != 4)
                        {
                            int MemberId = Convert.ToInt32(reader["MemberId"]);
                            string Name = reader["Name"].ToString();
                            string Email = reader["Email"].ToString();
                            string PhoneNumber = reader["PhoneNumber"].ToString();
                            DateTime Date = (DateTime)reader["Date"];
                            Membership membership = null;
                            //This is a Active account
                            if (MembershipId == 1)
                            {
                                membership = MembershipStatus[0];
                            }
                            //this is a passive account
                            else if (MembershipId == 2)
                            {
                                membership = MembershipStatus[1];
                            }
                            else
                            {
                                throw new Exception("MemberId: " + MemberId + " Has a MembershipId that is not valid");
                            }

                            Member member = new Member(MemberId, Name, Email, PhoneNumber, Date, membership);
                            members.Add(member);
                        }

                    }
                }

                CloseConnection();

                return members;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Member GetMemberById(int id)
        {
            try
            {
                Member member = null;

                string sql = $"SELECT * FROM Memberships; SELECT * FROM Members WHERE MemberId = {id};";

                
                SqlDataReader reader = Execute(sql);

                List<Membership> MembershipStatus = new List<Membership>();
                while (reader.Read())
                {
                    int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                    string MembershipName = reader["Name"].ToString();
                    string MembershipDescription = reader["Description"].ToString();

                    MembershipStatus.Add(new Membership(MembershipId, MembershipName, MembershipDescription));
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        int MemberId = Convert.ToInt32(reader["MemberId"]);
                        string Name = reader["Name"].ToString();
                        string Email = reader["Email"].ToString();
                        string PhoneNumber = reader["PhoneNumber"].ToString();
                        DateTime Date = (DateTime)reader["Date"];
                        int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                        Membership membership = null;
                        if (MembershipId == 1)
                        {
                            membership = MembershipStatus[0];
                        }
                        else if (MembershipId == 2)
                        {
                            membership = MembershipStatus[1];
                        }

                        else
                        {
                            throw new Exception(MemberId + "Has a MembershipId that is not valid");
                        }

                        member = new Member(MemberId, Name, Email, PhoneNumber, Date, membership);
                    }
                }
                
                CloseConnection();

                if (member == null)
                {
                    throw new Exception("Member not found");
                }
                return member;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int id, Member member)
        {
            try
            {
                string sql = $"UPDATE Members SET Name = '{member.Name}', PhoneNumber = '{member.PhoneNumber}', Email = '{member.Email}',Date = '{DateTime.Now.ToString("yyyy-MM-dd")}', MembershipId = {member.Membership.MembershipId} WHERE MemberId = {id}";

                SqlDataReader reader = Execute(sql);

                CloseConnection();               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Membership> GetMemberships()
        {
            try
            {
                string sql = "SELECT * FROM Memberships";

              
                SqlDataReader reader = Execute(sql);
                List<Membership> MembershipStatus = new List<Membership>();
                while (reader.Read())
                {
                    int MembershipId = Convert.ToInt32(reader["MembershipId"]);
                    string MembershipName = reader["Name"].ToString();
                    string MembershipDescription = reader["Description"].ToString();

                    MembershipStatus.Add(new Membership(MembershipId, MembershipName, MembershipDescription));
                }

                CloseConnection();

                return MembershipStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Remove(int id)
        {
            try
            {
                string sql = $"DELETE FROM Members WHERE memberId = {id}";

                SqlDataReader reader = Execute(sql);
                CloseConnection();

                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

    }
}
