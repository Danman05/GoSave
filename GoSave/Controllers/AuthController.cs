using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoSave.Models;
using GoSave.Repositories;
using GoSave.Services;
using GoSave.Context;
using System.Security.Principal;
using HackGame.Api;

namespace GoSave.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;
        UserRepo _userRepo = new UserRepo();
        GoSaveDbContext _db;

        public AuthController(JwtService jwtService, GoSaveDbContext db)
        {
            _jwtService = jwtService;
            this._db = db;
        }

        [AllowAnonymous]
        [HttpPut("Login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                Console.WriteLine(username + ":" + password + "asdfhnofgjoasefadj");
                var user = this._db.Identity.Where(i => i.Username == username.ToLower()).FirstOrDefault();
                if (user == null)
                {
                    return NotFound("username or password was íncorrect");
                }

                if (user.Password == PasswordHasher.HashPassword(password))
                {
                    return Ok();
                }

                return Unauthorized("username or password was íncorrect");
            }
            catch (Exception)
            {
                return BadRequest("Failed login - try again");
            }
        }

        [AllowAnonymous]
        [HttpPut("SignIn")]
        public async Task<IActionResult> Signup(string Username, string Password, string Email, string Firstname, string Lastname, string Street, string City, string Postalcode, string Country)
        {
            try
            {
                Console.WriteLine(Username);
                return Unauthorized("fuck off");
            }
            catch (Exception)
            {
                return BadRequest("Failed login - try again");
            }
        }
    }
}
