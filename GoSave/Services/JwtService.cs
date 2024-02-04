using GoSave.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoSave.Services
{
    /// <summary>
    /// Class 
    /// </summary>
    public class JwtService
    {
        // Empty fields, assigned in constructor
        private byte[] _JwtKey;
        private string _JwtIssuer;
        private string _JwtAudience;
        public JwtService(IConfiguration configuration)
        {
            // Get configuration values
            _JwtKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]);
            _JwtIssuer = configuration["JwtSettings:Key"];
            _JwtAudience = configuration["Jwt:Audience"];
        }


        public string GenerateJSONWebToken(User user)
        {
            // Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Identity.Username),
                
                /*  Custom claim
                 *  userId, helps keep track for vault context
                 *  
                */
                new Claim("userId", user.Id.ToString()) 
            };
            var securityKey = new SymmetricSecurityKey(_JwtKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_JwtIssuer,
              _JwtAudience,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
