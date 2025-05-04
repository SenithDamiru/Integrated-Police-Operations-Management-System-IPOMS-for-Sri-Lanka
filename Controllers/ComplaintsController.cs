using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using testproj.Data;
using testproj.Models;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints([FromQuery] int? citizenId)
        {
            if (citizenId.HasValue)
            {
                return await _context.Complaints.Where(c => c.CitizenID == citizenId.Value).ToListAsync();
            }
            return await _context.Complaints.ToListAsync();
        }


        // GET: api/Complaints/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return complaint;
        }

        // POST: api/Complaints
        [HttpPost]
        public async Task<ActionResult<Complaint>> CreateComplaint(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComplaint), new { id = complaint.ComplaintID }, complaint);
        }

        // PUT: api/Complaints/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComplaint(int id, Complaint complaint)
        {
            if (id != complaint.ComplaintID)
            {
                return BadRequest();
            }

            _context.Entry(complaint).State = EntityState.Modified;

            try
            {
                complaint.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Complaints/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.ComplaintID == id);
        }
    }
}
