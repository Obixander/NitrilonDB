using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace NitrilonDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllMemberships()
        {
            try
            {
                Repository repo = new();
                List<Membership> memberships = repo.GetMemberships();
                return Ok(memberships);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
