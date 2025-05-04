using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Data;

namespace testproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fine>>> GetFines()
        {
            return await _context.Fines.Include(f => f.Citizen).ToListAsync();
        }

        // GET: api/Fines/citizen/5
        // This is the new method that filters fines by CitizenID
        [HttpGet("citizen/{citizenId}")]
        public async Task<ActionResult<IEnumerable<Fine>>> GetFinesByCitizen(int citizenId)
        {
            var fines = await _context.Fines
                .Where(f => f.CitizenID == citizenId)
                .Include(f => f.Citizen)
                .ToListAsync();

            if (fines == null || fines.Count == 0)
            {
                return NotFound($"No fines found for CitizenID: {citizenId}");
            }

            return fines;
        }

        // GET: api/Fines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fine>> GetFine(int id)
        {
            var fine = await _context.Fines.FindAsync(id);

            if (fine == null)
            {
                return NotFound();
            }

            return fine;
        }

        // POST: api/Fines
        [HttpPost]
        public async Task<ActionResult<Fine>> PostFine(Fine fine)
        {
            _context.Fines.Add(fine);
            await _context.SaveChangesAsync();

            return StatusCode(201, fine); // Responds with 201 Created and the fine in the body
        }

        // PUT: api/Fines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFine(int id, Fine fine)
        {
            if (id != fine.FineID)
            {
                return BadRequest();
            }

            _context.Entry(fine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FineExists(id))
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

        // DELETE: api/Fines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFine(int id)
        {
            var fine = await _context.Fines.FindAsync(id);
            if (fine == null)
            {
                return NotFound(new { message = "Fine not found." });
            }

            if (fine.Status == "Paid")
            {
                return BadRequest(new { message = "This fine is already paid and cannot be deleted." });
            }

            _context.Fines.Remove(fine);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Fine successfully deleted." });
        }

        private bool FineExists(int id)
        {
            return _context.Fines.Any(e => e.FineID == id);
        }
    }
}
