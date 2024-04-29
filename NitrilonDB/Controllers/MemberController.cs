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
        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            try
            {
                Repository repo = new Repository();
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
                Repository repo = new Repository();
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
                Repository repo = new Repository();
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
                Repository repo = new Repository();
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
