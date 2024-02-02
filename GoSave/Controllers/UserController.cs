using GoSave.Data;
using GoSave.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSave.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        UserData _userData = new UserData();

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index([FromBody] User user)
        {
            try
            {

                if (_userData.AddUser(user))
                    return Ok(new { message = "User" });

                return BadRequest(new { message = "Error creating user", code = Response.HttpContext.Response.StatusCode });
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
