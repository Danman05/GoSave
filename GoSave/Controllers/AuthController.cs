using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoSave.Models;
using GoSave.Repositories;
using GoSave.Services;

namespace GoSave.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;
        UserRepo _userRepo = new UserRepo();

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Identity identity)
        {
            try
            {
               if (string.IsNullOrEmpty(identity.Username) || string.IsNullOrEmpty(identity.Password))
                    return BadRequest("Username or password is not valid");

                User loginResult = _userRepo.VerifyLogin(identity);

                if (loginResult == null) { return Unauthorized(); }

                string token = _jwtService.GenerateJSONWebToken(loginResult);
                return Ok(token);
            }
            catch (Exception)
            {
                return BadRequest("Failed login - try again");
            }
        }
    }
}
