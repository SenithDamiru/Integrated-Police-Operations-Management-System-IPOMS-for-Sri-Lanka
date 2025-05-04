using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using testproj.Data;
using testproj.Models;
using Microsoft.Extensions.Configuration;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Admin/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthModel authModel)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == authModel.Email);
            if (admin == null)
            {
                // Return Unauthorized with a custom error message if the email is not found
                return Unauthorized(new { message = "Email not found." });
            }

            if (admin.PasswordHash != authModel.PasswordHash)
            {
                // Return Unauthorized with a custom error message if the password is incorrect
                return Unauthorized(new { message = "Invalid password." });
            }

            // Create JWT Token if credentials are correct
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.AdminId.ToString()),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Role, "Admin") // Add role claim
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return lowercase "token" to match frontend expectations
            return Ok(new
            {
                token = tokenString,
                email = admin.Email,
                userRole = "admin",
                firstName = admin.FirstName,
                lastName = admin.LastName,
                adminId = admin.AdminId
            });
        }

        // GET: api/Admin
        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            var admins = _context.Admins.ToList();
            return Ok(admins);
        }

        // GET: api/Admin/1
        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null) return NotFound();
            return Ok(admin);
        }

        // POST: api/Admin
        [HttpPost]
        public IActionResult CreateAdmin([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Proceed with admin creation...

            if (admin == null) return BadRequest();

            admin.CreatedAt = DateTime.Now;
            admin.IsActive = true;

            _context.Admins.Add(admin);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAdminById), new { id = admin.AdminId }, admin);
        }

        // PUT: api/Admin/1
        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(int id, [FromBody] Admin updatedAdmin)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null) return NotFound();

            admin.FirstName = updatedAdmin.FirstName;
            admin.LastName = updatedAdmin.LastName;
            admin.Email = updatedAdmin.Email;
            admin.PasswordHash = updatedAdmin.PasswordHash;
            admin.PhoneNumber = updatedAdmin.PhoneNumber;
            admin.IsActive = updatedAdmin.IsActive;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Admin/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null) return NotFound();

            _context.Admins.Remove(admin);
            _context.SaveChanges();

            return NoContent();
        }
    }
}