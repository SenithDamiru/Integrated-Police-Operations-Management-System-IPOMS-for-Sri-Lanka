using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testproj.Data;
using testproj.Models;

[Route("api/[controller]")]
[ApiController]
public class RobberyReportsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RobberyReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/RobberyReports
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RobberyReport>>> GetRobberyReports()
    {
        return await _context.RobberyReports
            .Include(r => r.Citizen)
            .Include(r => r.Responder)
            .ToListAsync();
    }

    // GET: api/RobberyReports/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RobberyReport>> GetRobberyReport(int id)
    {
        var robberyReport = await _context.RobberyReports
            .Include(r => r.Citizen)
            .Include(r => r.Responder)
            .FirstOrDefaultAsync(r => r.ReportID == id);

        if (robberyReport == null)
        {
            return NotFound($"No robbery report found with ID: {id}");
        }

        return robberyReport;
    }

    // GET: api/RobberyReports/citizen/8
    [HttpGet("citizen/{citizenId}")]
    public async Task<ActionResult<IEnumerable<RobberyReport>>> GetRobberyReportsByCitizen(int citizenId)
    {
        var reports = await _context.RobberyReports
            .Where(r => r.CitizenID == citizenId)
            .Include(r => r.Citizen)
            .Include(r => r.Responder)
            .ToListAsync();

        if (reports == null || reports.Count == 0)
        {
            return NotFound($"No robbery reports found for CitizenID: {citizenId}");
        }

        return reports;
    }

    // POST: api/RobberyReports
    [HttpPost]
    public async Task<ActionResult<RobberyReport>> CreateRobberyReport(RobberyReport report)
    {
        _context.RobberyReports.Add(report);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRobberyReport), new { id = report.ReportID }, report);
    }

    // PUT: api/RobberyReports/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRobberyReport(int id, RobberyReport report)
    {
        if (id != report.ReportID)
        {
            return BadRequest("Report ID mismatch.");
        }

        _context.Entry(report).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.RobberyReports.Any(r => r.ReportID == id))
            {
                return NotFound($"No robbery report found with ID: {id}");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/RobberyReports/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRobberyReport(int id)
    {
        var report = await _context.RobberyReports.FindAsync(id);
        if (report == null)
        {
            return NotFound($"No robbery report found with ID: {id}");
        }

        _context.RobberyReports.Remove(report);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
