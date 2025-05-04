using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using testproj.Data;
using testproj.Models;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliceOfficerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PoliceOfficerController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Login DTO (Data Transfer Object)
        public class LoginDto
        {
            public string Email { get; set; }
            public string PasswordHash { get; set; }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                // Find the police officer by email
                var officer = await _context.PoliceOfficers
                    .Include(o => o.Role)
                    .FirstOrDefaultAsync(o => o.Email == loginDto.Email);

                // Check if officer exists
                if (officer == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Validate password 
                // IMPORTANT: Replace with secure password verification
                // In a real-world scenario, use proper password hashing comparison
                if (officer.PasswordHash != loginDto.PasswordHash)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Check if officer is active
                if (!officer.IsActive)
                {
                    return Unauthorized(new { message = "Account is not active" });
                }

                // Generate JWT Token
                var token = GenerateJwtToken(officer);

                // Return login response with lowercase properties to match frontend expectations
                return Ok(new
                {
                    token = token,
                    email = officer.Email,
                    officerID = officer.OfficerID,
                    userRole = "officer",
                    fullName = officer.FullName,
                    roleID = officer.RoleID,
                    roleName = officer.Role?.RoleName ?? "Unknown",
                    stationID = officer.StationID
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred during login" });
            }
        }

        private string GenerateJwtToken(PoliceOfficer officer)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, officer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("OfficerID", officer.OfficerID.ToString()),
                new Claim(ClaimTypes.Role, "PoliceOfficer")
            };

            var token = new JwtSecurityToken(
                // Note: Issuer and Audience are set to false in your configuration
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}