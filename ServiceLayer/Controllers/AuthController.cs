using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventManagement.Repository;
using EventManagement.Model;

namespace ServiceLayer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.EmailId) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("EmailId and Password are required.");
            }

            var user = _userRepository.Get(model.EmailId);

            if (user == null || user.Password != model.Password)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.EmailId)
            };

            // Add role only if it's not null
            if (!string.IsNullOrEmpty(user.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

            
        }
    }
}
