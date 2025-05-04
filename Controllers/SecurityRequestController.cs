using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Data;
using testproj.Models;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityRequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SecurityRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Create a Security Request (POST)
        [HttpPost]
        public async Task<IActionResult> CreateSecurityRequest([FromBody] SecurityRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Validation: Ensure the SecurityDate is in the future
            if (request.SecurityDate < DateTime.Now)
            {
                return BadRequest("Security date must be in the future.");
            }

            try
            {
                _context.SecurityRequests.Add(request);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSecurityRequestById), new { id = request.RequestID }, request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // 2. Get All Security Requests (GET) with optional filtering by CitizenID
        [HttpGet]
        public async Task<IActionResult> GetAllSecurityRequests([FromQuery] int? citizenId)
        {
            IQueryable<SecurityRequest> query = _context.SecurityRequests;

            // Apply filter if CitizenID is provided
            if (citizenId.HasValue)
            {
                query = query.Where(r => r.CitizenID == citizenId.Value);
            }

            var requests = await query.ToListAsync();
            return Ok(requests);
        }

        // 3. Get Security Request by ID (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecurityRequestById(int id)
        {
            var request = await _context.SecurityRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound("Security request not found.");
            }

            return Ok(request);
        }

        // 4. Update a Security Request (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSecurityRequest(int id, [FromBody] SecurityRequest updatedRequest)
        {
            if (id != updatedRequest.RequestID)
            {
                return BadRequest("Request ID mismatch.");
            }

            var existingRequest = await _context.SecurityRequests.FindAsync(id);
            if (existingRequest == null)
            {
                return NotFound("Security request not found.");
            }

            // Validation: Ensure the SecurityDate is in the future
            if (updatedRequest.SecurityDate < DateTime.Now)
            {
                return BadRequest("Security date must be in the future.");
            }

            // Update fields
            existingRequest.RequestReason = updatedRequest.RequestReason;
            existingRequest.Location = updatedRequest.Location;
            existingRequest.SecurityDate = updatedRequest.SecurityDate;
            existingRequest.Description = updatedRequest.Description;
            existingRequest.Status = updatedRequest.Status;
            existingRequest.AssignedOfficerID = updatedRequest.AssignedOfficerID;

            await _context.SaveChangesAsync();

            return NoContent(); // No content as the update is successful
        }

        // 5. Delete a Security Request (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurityRequest(int id)
        {
            var request = await _context.SecurityRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound("Security request not found.");
            }

            _context.SecurityRequests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent(); // Successfully deleted
        }
    }
}
