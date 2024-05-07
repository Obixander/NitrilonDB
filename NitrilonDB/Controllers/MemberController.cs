using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace NitrilonDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {


        [HttpDelete("{id}")]
        public IActionResult RemoveMember(int id)
        {
            try
            {
                MemberRepository repo = new();
                repo.Remove(id);
               return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            try
            {
                MemberRepository repo = new MemberRepository();
                int newId = repo.Add(member);
                return Ok(newId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetALlMembers()
        {
            try
            {
                MemberRepository repo = new MemberRepository();
                List<Member> members = repo.GetAllMembers();
                return Ok(members);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            try
            {
                MemberRepository repo = new MemberRepository();
                Member member = repo.GetMemberById(id);
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, Member member)
        {
            try
            {
                MemberRepository repo = new MemberRepository();
                repo.Update(id, member);
                return Ok("Member updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
