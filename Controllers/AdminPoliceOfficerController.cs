using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testproj.Data;
using testproj.Models;
using System.Linq;
using testproj.DTOs;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // This ensures only Admin role can access this controller
    public class AdminPoliceOfficerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminPoliceOfficerController> _logger;

        public AdminPoliceOfficerController(ApplicationDbContext context, ILogger<AdminPoliceOfficerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/AdminPoliceOfficer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoliceOfficerDto>>> GetOfficers()
        {
            try
            {
                var officers = await _context.PoliceOfficers
                    .Include(o => o.Role)
                    .Include(o => o.Station)
                    .Select(o => new PoliceOfficerDto
                    {
                        OfficerID = o.OfficerID,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNumber = o.PhoneNumber,
                        RoleID = o.RoleID,
                        RoleName = o.Role.RoleName,
                        StationID = o.StationID,
                        StationName = o.Station.StationName,
                        IsActive = o.IsActive,
                        DateJoined = o.DateJoined
                    })
                    .ToListAsync();

                return Ok(officers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting police officers");
                return StatusCode(500, new { message = "Error retrieving officers" });
            }
        }

        // GET: api/AdminPoliceOfficer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PoliceOfficerDto>> GetOfficer(int id)
        {
            try
            {
                var officer = await _context.PoliceOfficers
                    .Include(o => o.Role)
                    .Include(o => o.Station)
                    .Where(o => o.OfficerID == id)
                    .Select(o => new PoliceOfficerDto
                    {
                        OfficerID = o.OfficerID,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNumber = o.PhoneNumber,
                        RoleID = o.RoleID,
                        RoleName = o.Role.RoleName,
                        StationID = o.StationID,
                        StationName = o.Station.StationName,
                        IsActive = o.IsActive,
                        DateJoined = o.DateJoined
                    })
                    .FirstOrDefaultAsync();

                if (officer == null)
                {
                    return NotFound(new { message = "Officer not found" });
                }

                return Ok(officer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting police officer with ID {id}");
                return StatusCode(500, new { message = "Error retrieving officer details" });
            }
        }

        // GET: api/AdminPoliceOfficer/Roles
        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<PoliceRole>>> GetRoles()
        {
            try
            {
                var roles = await _context.PoliceRoles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting police roles");
                return StatusCode(500, new { message = "Error retrieving roles" });
            }
        }

        // GET: api/AdminPoliceOfficer/Stations
        [HttpGet("Stations")]
        public async Task<ActionResult<IEnumerable<PoliceStation>>> GetStations()
        {
            try
            {
                var stations = await _context.PoliceStations.ToListAsync();
                return Ok(stations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting police stations");
                return StatusCode(500, new { message = "Error retrieving stations" });
            }
        }

        // POST: api/AdminPoliceOfficer
        [HttpPost]
        public async Task<ActionResult<PoliceOfficerDto>> CreateOfficer(PoliceOfficerInputDto inputDto)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrEmpty(inputDto.FullName) || string.IsNullOrEmpty(inputDto.Email) || string.IsNullOrEmpty(inputDto.PasswordHash))
                {
                    return BadRequest(new { message = "Name, email, and password are required" });
                }

                // Check if email is already in use
                if (await _context.PoliceOfficers.AnyAsync(o => o.Email == inputDto.Email))
                {
                    return BadRequest(new { message = "Email is already in use" });
                }

                // Validate RoleID
                if (!await _context.PoliceRoles.AnyAsync(r => r.RoleID == inputDto.RoleID))
                {
                    return BadRequest(new { message = "Invalid Role ID" });
                }

                // Validate StationID
                if (!await _context.PoliceStations.AnyAsync(s => s.StationID == inputDto.StationID))
                {
                    return BadRequest(new { message = "Invalid Station ID" });
                }

                // Create new officer entity
                var officer = new PoliceOfficer
                {
                    FullName = inputDto.FullName,
                    Email = inputDto.Email,
                    PhoneNumber = inputDto.PhoneNumber,
                    PasswordHash = inputDto.PasswordHash,
                    RoleID = inputDto.RoleID,
                    StationID = inputDto.StationID,
                    IsActive = inputDto.IsActive,
                    DateJoined = inputDto.DateJoined == default ? DateTime.Now : inputDto.DateJoined
                };

                _context.PoliceOfficers.Add(officer);
                await _context.SaveChangesAsync();

                // Return the created officer as DTO
                var createdOfficer = await _context.PoliceOfficers
                    .Include(o => o.Role)
                    .Include(o => o.Station)
                    .Where(o => o.OfficerID == officer.OfficerID)
                    .Select(o => new PoliceOfficerDto
                    {
                        OfficerID = o.OfficerID,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNumber = o.PhoneNumber,
                        RoleID = o.RoleID,
                        RoleName = o.Role.RoleName,
                        StationID = o.StationID,
                        StationName = o.Station.StationName,
                        IsActive = o.IsActive,
                        DateJoined = o.DateJoined
                    })
                    .FirstOrDefaultAsync();

                return CreatedAtAction(nameof(GetOfficer), new { id = officer.OfficerID }, createdOfficer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating police officer: " + ex.Message);
                return StatusCode(500, new { message = "Error creating officer: " + ex.Message });
            }
        }

        // PUT: api/AdminPoliceOfficer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOfficer(int id, PoliceOfficerInputDto inputDto)
        {
            try
            {
                if (id != inputDto.OfficerID)
                {
                    return BadRequest(new { message = "ID mismatch" });
                }

                // Basic validation
                if (string.IsNullOrEmpty(inputDto.FullName) || string.IsNullOrEmpty(inputDto.Email))
                {
                    return BadRequest(new { message = "Name and email are required" });
                }

                // Check if email is already in use by another officer
                var existingOfficer = await _context.PoliceOfficers
                    .FirstOrDefaultAsync(o => o.Email == inputDto.Email && o.OfficerID != id);

                if (existingOfficer != null)
                {
                    return BadRequest(new { message = "Email is already in use" });
                }

                // Validate RoleID
                if (!await _context.PoliceRoles.AnyAsync(r => r.RoleID == inputDto.RoleID))
                {
                    return BadRequest(new { message = "Invalid Role ID" });
                }

                // Validate StationID
                if (!await _context.PoliceStations.AnyAsync(s => s.StationID == inputDto.StationID))
                {
                    return BadRequest(new { message = "Invalid Station ID" });
                }

                // Get the existing officer to check if it exists
                var officerToUpdate = await _context.PoliceOfficers.FindAsync(id);
                if (officerToUpdate == null)
                {
                    return NotFound(new { message = "Officer not found" });
                }

                // Update fields
                officerToUpdate.FullName = inputDto.FullName;
                officerToUpdate.Email = inputDto.Email;
                officerToUpdate.PhoneNumber = inputDto.PhoneNumber;
                officerToUpdate.RoleID = inputDto.RoleID;
                officerToUpdate.StationID = inputDto.StationID;
                officerToUpdate.IsActive = inputDto.IsActive;

                // Only update password if provided
                if (!string.IsNullOrEmpty(inputDto.PasswordHash))
                {
                    officerToUpdate.PasswordHash = inputDto.PasswordHash;
                }

                // Update DateJoined if provided
                if (inputDto.DateJoined != default)
                {
                    officerToUpdate.DateJoined = inputDto.DateJoined;
                }

                _context.Entry(officerToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Return the updated officer as DTO
                var updatedOfficerDto = await _context.PoliceOfficers
                    .Include(o => o.Role)
                    .Include(o => o.Station)
                    .Where(o => o.OfficerID == id)
                    .Select(o => new PoliceOfficerDto
                    {
                        OfficerID = o.OfficerID,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNumber = o.PhoneNumber,
                        RoleID = o.RoleID,
                        RoleName = o.Role.RoleName,
                        StationID = o.StationID,
                        StationName = o.Station.StationName,
                        IsActive = o.IsActive,
                        DateJoined = o.DateJoined
                    })
                    .FirstOrDefaultAsync();

                return Ok(updatedOfficerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating police officer with ID {id}: {ex.Message}");
                return StatusCode(500, new { message = "Error updating officer: " + ex.Message });
            }
        }

        // DELETE: api/AdminPoliceOfficer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficer(int id)
        {
            try
            {
                var officer = await _context.PoliceOfficers.FindAsync(id);
                if (officer == null)
                {
                    return NotFound(new { message = "Officer not found" });
                }

                _context.PoliceOfficers.Remove(officer);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Officer deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting police officer with ID {id}: {ex.Message}");
                return StatusCode(500, new { message = "Error deleting officer: " + ex.Message });
            }
        }
    }
}