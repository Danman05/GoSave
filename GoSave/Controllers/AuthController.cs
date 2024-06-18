﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoSave.Repositories;
using GoSave.Services;
using GoSave.Context;
using HackGame.Api;
using GoSave.Models;
using System.Data.Entity;

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
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                Console.WriteLine(username + ":" + password + "asdfhnofgjoasefadj");
                var identity = this._db.Identity.Where(i => i.Username == username.ToLower()).FirstOrDefault();
                if (identity == null)
                {
                    return NotFound("username or password was íncorrect");
                }

                if (identity.Password == PasswordHasher.HashPassword(password))
                {
                    var user = _db.Users.Where(i => i.IdentityId == identity.Id).FirstOrDefault();
                    if(user == null)
                    {
                        return NotFound();
                    }
                    return Ok(_jwtService.GenerateJSONWebToken(identity));
                }

                return NotFound("username or password was íncorrect");
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
                if(_db.Identity.Where(i=>i.Username == Username.ToLower()).Any())
                {
                    return BadRequest("Username or email in use");
                }

                var identity = new Identity(Username.ToLower(), PasswordHasher.HashPassword(Password));
                var address = new Address() { City = City, Id = Guid.NewGuid(), Country = Country, PostalCode = Postalcode, Street = Street };
                var newUser = new User(Firstname, Lastname, address, identity.Id);
                await _db.AddAsync(identity);
                await this._db.AddAsync(newUser);
                await this._db.SaveChangesAsync();
                return Ok(this._jwtService.GenerateJSONWebToken(identity));
                //send verification email
            }
            catch (Exception)
            {
                return BadRequest("Failed login - try again");  
            }
        }
    }
}
