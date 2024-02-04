using GoSave.Data;
using GoSave.Models;
using GoSave.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSave.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        ILogger<UserController> _logger;

        UserRepo _userRepo = new UserRepo();
        public UserController(ILogger<UserController> logger)
        {
           _logger = logger;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                _userRepo.AddUser(user);
                _logger.LogInformation("user has been created with Id: {userId}", user.Id);
                return Ok(new { message = "User created", createdUser = user});

            }
            // Catches errrors -
            // null exception: Fields are null
            // InvalidOperation: Username already exists
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Failed to create user reason: Empty field for username or password");
                return BadRequest(new { message = ex.Message });
            }

            catch (InvalidOperationException ex)
            {
                _logger.LogError("Failed to create user reason: user already exist");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unknown error has occoured");
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
