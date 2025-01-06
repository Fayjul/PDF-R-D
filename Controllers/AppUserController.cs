using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using MongoDB.Driver;
using PDF.Configurations;
using PDF.Models;

namespace PDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AppUserController(IMongoDatabase database, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = new UserRepository(database);
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Name);
            if (existingUser != null)
                return BadRequest("Username already exists");
            var appuser = new AppUser();
            appuser.Name = user.Name;
            appuser.PasswordHash = UserRepository.HashPassword(user.Password);
            await _userRepository.CreateUserAsync(appuser);

            return Ok("User registered successfully");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Name);
            if (existingUser == null || existingUser.PasswordHash != UserRepository.HashPassword(user.Password))
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(existingUser);
            return Ok(new { Token = token });
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult ProtectedRoute()
        {
            return Ok("This is a protected route, only accessible with a valid JWT.");
        }
        private string GenerateJwtToken(AppUser user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserId", user.Id ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
