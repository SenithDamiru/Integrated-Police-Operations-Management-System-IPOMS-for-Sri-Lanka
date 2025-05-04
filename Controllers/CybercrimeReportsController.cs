using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Data;
using testproj.Models;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CybercrimeReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CybercrimeReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CybercrimeReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CybercrimeReport>>> GetCybercrimeReports([FromQuery] int? citizenId)
        {
            if (citizenId.HasValue)
            {
                return await _context.CybercrimeReports
                    .Where(r => r.CitizenID == citizenId.Value)
                    .ToListAsync();
            }
            return await _context.CybercrimeReports.ToListAsync();
        }

        // GET: api/CybercrimeReports/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CybercrimeReport>> GetCybercrimeReport(int id)
        {
            var report = await _context.CybercrimeReports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // POST: api/CybercrimeReports
        [HttpPost]
        public async Task<ActionResult<CybercrimeReport>> CreateCybercrimeReport(CybercrimeReport report)
        {
            report.DateReported = DateTime.Now;
            report.LastUpdated = DateTime.Now;

            _context.CybercrimeReports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCybercrimeReport), new { id = report.ReportID }, report);
        }

        // PUT: api/CybercrimeReports/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCybercrimeReport(int id, CybercrimeReport report)
        {
            if (id != report.ReportID)
            {
                return BadRequest();
            }

            report.LastUpdated = DateTime.Now;
            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CybercrimeReportExists(id))
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

        // DELETE: api/CybercrimeReports/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCybercrimeReport(int id)
        {
            var report = await _context.CybercrimeReports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.CybercrimeReports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CybercrimeReportExists(int id)
        {
            return _context.CybercrimeReports.Any(e => e.ReportID == id);
        }
    }
}