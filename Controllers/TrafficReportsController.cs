using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testproj.Data;
using testproj.Models;

[Route("api/[controller]")]
[ApiController]
public class TrafficReportsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TrafficReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/TrafficReports
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrafficReport>>> GetTrafficReports()
    {
        return await _context.TrafficReports
            .Include(t => t.Citizen) // Include Citizen details
            .Include(t => t.Responder) // Include Responder details (nullable)
            .ToListAsync();
    }

    // GET: api/TrafficReports/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TrafficReport>> GetTrafficReport(int id)
    {
        var trafficReport = await _context.TrafficReports
            .Include(t => t.Citizen)
            .Include(t => t.Responder)
            .FirstOrDefaultAsync(t => t.ReportID == id);

        if (trafficReport == null)
        {
            return NotFound($"No traffic report found with ID: {id}");
        }

        return trafficReport;
    }

    // GET: api/TrafficReports/citizen/8
    [HttpGet("citizen/{citizenId}")]
    public async Task<ActionResult<IEnumerable<TrafficReport>>> GetTrafficReportsByCitizen(int citizenId)
    {
        var reports = await _context.TrafficReports
            .Where(t => t.CitizenID == citizenId)
            .Include(t => t.Citizen)
            .Include(t => t.Responder)
            .ToListAsync();

        if (reports == null || reports.Count == 0)
        {
            return NotFound($"No traffic reports found for CitizenID: {citizenId}");
        }

        return reports;
    }

    // POST: api/TrafficReports
    [HttpPost]
    public async Task<ActionResult<TrafficReport>> CreateTrafficReport(TrafficReport report)
    {
        _context.TrafficReports.Add(report);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrafficReport), new { id = report.ReportID }, report);
    }

    // PUT: api/TrafficReports/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrafficReport(int id, TrafficReport report)
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
            if (!_context.TrafficReports.Any(t => t.ReportID == id))
            {
                return NotFound($"No traffic report found with ID: {id}");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/TrafficReports/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrafficReport(int id)
    {
        var report = await _context.TrafficReports.FindAsync(id);
        if (report == null)
        {
            return NotFound($"No traffic report found with ID: {id}");
        }

        _context.TrafficReports.Remove(report);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
