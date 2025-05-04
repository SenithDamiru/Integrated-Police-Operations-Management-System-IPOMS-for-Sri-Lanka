using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Data;
using testproj.Models;
using Microsoft.Extensions.Logging;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmergencyController> _logger;

        public EmergencyController(ApplicationDbContext context, ILogger<EmergencyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Emergency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmergencyRequest>>> GetEmergencyRequests()
        {

            var requests = await _context.EmergencyRequests
                .Include(r => r.Citizen)    // Include Citizen navigation property
                .Include(r => r.Officer)    // Include Officer navigation property
                .ToListAsync();

            return Ok(requests);
        }

        // GET: api/Emergency/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmergencyRequest>> GetEmergencyRequestById(int id)
        {
            var request = await _context.EmergencyRequests
                .Include(r => r.Citizen)    // Include Citizen navigation property
                .Include(r => r.Officer)    // Include Officer navigation property
                .FirstOrDefaultAsync(r => r.EmergencyID == id);

            if (request == null)
            {
                return NotFound("Emergency request not found.");
            }

            return Ok(request);
        }

        // POST: api/Emergency
        [HttpPost]
        public async Task<IActionResult> CreateEmergencyRequest([FromBody] EmergencyRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Validation: Ensure required fields are not empty
            if (string.IsNullOrWhiteSpace(request.RequestType) ||
                string.IsNullOrWhiteSpace(request.Location))
            {
                return BadRequest("Request type and location are required.");
            }

            // Validate CitizenID if provided
            if (request.CitizenID.HasValue)
            {
                _logger.LogInformation($"Validating CitizenID: {request.CitizenID.Value}");

                var allCitizens = await _context.Citizens
                    .Select(c => new { c.CitizenID, c.FullName })
                    .ToListAsync();

                var citizenExists = allCitizens.Any(c => c.CitizenID == request.CitizenID.Value);

                if (!citizenExists)
                {
                    var availableIds = string.Join(", ", allCitizens.Select(c => $"{c.CitizenID} ({c.FullName})"));
                    _logger.LogWarning($"Citizen with ID {request.CitizenID.Value} not found. Available IDs: {availableIds}");

                    return BadRequest($"Citizen with ID {request.CitizenID.Value} does not exist. Available citizens: {availableIds}");
                }
            }

            // Validate OfficerID if provided
            if (request.OfficerID.HasValue)
            {
                _logger.LogInformation($"Validating OfficerID: {request.OfficerID.Value}");

                var allOfficers = await _context.PoliceOfficers
                    .Select(o => new { o.OfficerID, o.FullName })
                    .ToListAsync();

                var officerExists = allOfficers.Any(o => o.OfficerID == request.OfficerID.Value);

                if (!officerExists)
                {
                    var availableIds = string.Join(", ", allOfficers.Select(o => $"{o.OfficerID} ({o.FullName})"));
                    _logger.LogWarning($"Officer with ID {request.OfficerID.Value} not found. Available IDs: {availableIds}");

                    return BadRequest($"Police Officer with ID {request.OfficerID.Value} does not exist. Available officers: {availableIds}");
                }
            }

            try
            {
                // Set default values
                request.RequestTime = DateTime.Now;
                request.Status = "Pending";

                _context.EmergencyRequests.Add(request);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmergencyRequestById), new { id = request.EmergencyID }, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating emergency request");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Emergency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmergencyRequest(int id, [FromBody] EmergencyRequest updatedRequest)
        {
            if (id != updatedRequest.EmergencyID)
            {
                return BadRequest("Request ID mismatch.");
            }

            var existingRequest = await _context.EmergencyRequests.FindAsync(id);
            if (existingRequest == null)
            {
                return NotFound("Emergency request not found.");
            }

            // Validate CitizenID if provided
            if (updatedRequest.CitizenID.HasValue)
            {
                _logger.LogInformation($"Validating CitizenID: {updatedRequest.CitizenID.Value}");

                var allCitizens = await _context.Citizens
                    .Select(c => new { c.CitizenID, c.FullName })
                    .ToListAsync();

                var citizenExists = allCitizens.Any(c => c.CitizenID == updatedRequest.CitizenID.Value);

                if (!citizenExists)
                {
                    var availableIds = string.Join(", ", allCitizens.Select(c => $"{c.CitizenID} ({c.FullName})"));
                    _logger.LogWarning($"Citizen with ID {updatedRequest.CitizenID.Value} not found. Available IDs: {availableIds}");

                    return BadRequest($"Citizen with ID {updatedRequest.CitizenID.Value} does not exist. Available citizens: {availableIds}");
                }
            }

            // Validate OfficerID if provided
            if (updatedRequest.OfficerID.HasValue)
            {
                _logger.LogInformation($"Validating OfficerID: {updatedRequest.OfficerID.Value}");

                var allOfficers = await _context.PoliceOfficers
                    .Select(o => new { o.OfficerID, o.FullName })
                    .ToListAsync();

                var officerExists = allOfficers.Any(o => o.OfficerID == updatedRequest.OfficerID.Value);

                if (!officerExists)
                {
                    var availableIds = string.Join(", ", allOfficers.Select(o => $"{o.OfficerID} ({o.FullName})"));
                    _logger.LogWarning($"Officer with ID {updatedRequest.OfficerID.Value} not found. Available IDs: {availableIds}");

                    return BadRequest($"Police Officer with ID {updatedRequest.OfficerID.Value} does not exist. Available officers: {availableIds}");
                }
            }

            try
            {
                // Update fields
                existingRequest.RequestType = updatedRequest.RequestType;
                existingRequest.Location = updatedRequest.Location;
                existingRequest.ContactNumber = updatedRequest.ContactNumber;
                existingRequest.Description = updatedRequest.Description;
                existingRequest.Status = updatedRequest.Status;
                existingRequest.CitizenID = updatedRequest.CitizenID;
                existingRequest.OfficerID = updatedRequest.OfficerID;
                existingRequest.ResolutionNotes = updatedRequest.ResolutionNotes;

                // Update RespondedTime if needed
                if (updatedRequest.Status == "Dispatched" && existingRequest.RespondedTime == null)
                {
                    existingRequest.RespondedTime = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating emergency request");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Emergency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmergencyRequest(int id)
        {
            var request = await _context.EmergencyRequests.FindAsync(id);
            if (request == null)
            {
                return NotFound("Emergency request not found.");
            }

            try
            {
                _context.EmergencyRequests.Remove(request);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting emergency request {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Emergency/Citizens
        [HttpGet("Citizens")]
        public async Task<ActionResult<IEnumerable<object>>> GetCitizens()
        {
            var citizens = await _context.Citizens
                .Select(c => new { c.CitizenID, c.FullName })
                .ToListAsync();

            return Ok(citizens);
        }

        [HttpGet("Officers")]
        public async Task<ActionResult<IEnumerable<object>>> GetOfficers()
        {
            var officers = await _context.PoliceOfficers
                .Select(o => new { o.OfficerID, o.FullName })
                .ToListAsync();

            return Ok(officers);
        }
    }
}