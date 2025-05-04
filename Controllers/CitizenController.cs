using Microsoft.AspNetCore.Mvc;
using testproj.Data;
using testproj.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CitizenController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = "error", message = "Invalid input" });

            // Find user by email
            var citizen = _context.Citizens.FirstOrDefault(c => c.Email == loginRequest.Email);
            if (citizen == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, citizen.PasswordHash))
                return Unauthorized(new { status = "error", message = "Invalid email or password" });

            if (!citizen.IsActive)
                return Unauthorized(new { status = "error", message = "Account is inactive" });

            // Generate JWT token
            var token = GenerateJwtToken(citizen);

            return Ok(new
            {
                status = "success",
                message = "Login successful",
                token,
                user = new
                {
                    citizen.CitizenID,
                    citizen.FullName,
                    citizen.Email
                }
            });
        }

        private string GenerateJwtToken(Citizen citizen)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, citizen.CitizenID.ToString()),
                    new Claim(ClaimTypes.Email, citizen.Email),
                    new Claim(ClaimTypes.Name, citizen.FullName)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        public IActionResult GetAllCitizens()
        {
            var citizens = _context.Citizens.ToList();
            return Ok(citizens);
        }

        [HttpGet("{id}")]
        public IActionResult GetCitizenById(int id)
        {
            var citizen = _context.Citizens.Find(id);
            if (citizen == null) return NotFound();
            return Ok(citizen);
        }

        [HttpPost]
        public IActionResult CreateCitizen([FromBody] Citizen citizen)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (citizen == null) return BadRequest();

            citizen.PasswordHash = BCrypt.Net.BCrypt.HashPassword(citizen.PasswordHash);
            citizen.CreatedAt = DateTime.Now;
            citizen.UpdatedAt = DateTime.Now;

            _context.Citizens.Add(citizen);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCitizenById), new { id = citizen.CitizenID }, citizen);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCitizen(int id, [FromBody] Citizen updatedCitizen)
        {
            var citizen = _context.Citizens.Find(id);
            if (citizen == null) return NotFound();

            citizen.FullName = updatedCitizen.FullName;
            citizen.Email = updatedCitizen.Email;
            citizen.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedCitizen.PasswordHash);
            citizen.PhoneNumber = updatedCitizen.PhoneNumber;
            citizen.Address = updatedCitizen.Address;
            citizen.DateOfBirth = updatedCitizen.DateOfBirth;
            citizen.NationalID = updatedCitizen.NationalID;
            citizen.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCitizen(int id)
        {
            var citizen = _context.Citizens.Find(id);
            if (citizen == null) return NotFound();

            _context.Citizens.Remove(citizen);
            _context.SaveChanges();

            return NoContent();
        }
    }
}