using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using testproj.Data;
using testproj.Models;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliceReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PoliceReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PoliceReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoliceReport>>> GetPoliceReports()
        {
            var reports = await _context.PoliceReports
                                         .Include(r => r.Citizen) // Include citizen details
                                         .ToListAsync();
            return Ok(reports);
        }

        // GET: api/PoliceReports/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PoliceReport>> GetPoliceReport(int id)
        {
            var report = await _context.PoliceReports
                                        .Include(r => r.Citizen) // Include citizen details
                                        .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        // POST: api/PoliceReports
        [HttpPost]
        public async Task<ActionResult<PoliceReport>> CreatePoliceReport([FromBody] PoliceReport report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate if Citizen exists
            var citizen = await _context.Citizens.FindAsync(report.CitizenID);
            if (citizen == null)
            {
                return NotFound("Citizen not found.");
            }

            _context.PoliceReports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoliceReport", new { id = report.Id }, report);
        }

        // PUT: api/PoliceReports/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoliceReport(int id, [FromBody] PoliceReport report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PoliceReports/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliceReport(int id)
        {
            var report = await _context.PoliceReports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.PoliceReports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/PoliceReports/Citizen/{citizenId}
        [HttpGet("Citizen/{citizenId}")]
        public async Task<ActionResult<IEnumerable<PoliceReport>>> GetReportsByCitizen(int citizenId)
        {
            var reports = await _context.PoliceReports
                                         .Where(r => r.CitizenID == citizenId)
                                         .ToListAsync();

            if (!reports.Any())
            {
                return NotFound("No reports found for this citizen.");
            }

            return Ok(reports);
        }
    }
}