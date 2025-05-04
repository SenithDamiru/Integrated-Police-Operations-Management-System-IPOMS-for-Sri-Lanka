using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using testproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Data;
using testproj.Models.testproj.Models;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccidentReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccidentReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccidentReport>>> GetAccidentReports()
        {
            return await _context.AccidentReports
                .Include(a => a.Citizen) // Include Citizen details
                .Include(a => a.Responder) // Include Responder details
                .ToListAsync();
        }

        // GET: api/AccidentReports/8
        [HttpGet("{citizenId}")]
        public async Task<ActionResult<IEnumerable<AccidentReport>>> GetAccidentReportsByCitizen(int citizenId)
        {
            var accidentReports = await _context.AccidentReports
                .Where(a => a.CitizenID == citizenId)
               
                .ToListAsync();

            if (accidentReports == null || accidentReports.Count == 0)
            {
                return NotFound($"No accident reports found for CitizenID: {citizenId}");
            }

            return accidentReports;
        }


        // POST: api/AccidentReports
        [HttpPost]
        public async Task<ActionResult<AccidentReport>> PostAccidentReport(AccidentReport accidentReport)
        {
            _context.AccidentReports.Add(accidentReport);
            await _context.SaveChangesAsync();

            return StatusCode(201, accidentReport); // Responds with 201 Created and the accident report in the body
        }

        // PUT: api/AccidentReports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccidentReport(int id, AccidentReport accidentReport)
        {
            if (id != accidentReport.ReportID)
            {
                return BadRequest();
            }

            _context.Entry(accidentReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccidentReportExists(id))
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

        // DELETE: api/AccidentReports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccidentReport>> DeleteAccidentReport(int id)
        {
            var accidentReport = await _context.AccidentReports.FindAsync(id);
            if (accidentReport == null)
            {
                return NotFound();
            }

            _context.AccidentReports.Remove(accidentReport);
            await _context.SaveChangesAsync();

            return accidentReport;
        }

        private bool AccidentReportExists(int id)
        {
            return _context.AccidentReports.Any(e => e.ReportID == id);
        }
    }
}