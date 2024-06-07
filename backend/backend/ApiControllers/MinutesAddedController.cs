using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MinutesAddedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MinutesAddedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MinutesAdded
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MinutesAdded>>> GetMinutesAddeds()
        {
            return await _context.MinutesAddeds.ToListAsync();
        }

        // GET: api/MinutesAdded/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MinutesAdded>> GetMinutesAdded(Guid id)
        {
            var minutesAdded = await _context.MinutesAddeds.FindAsync(id);

            if (minutesAdded == null)
            {
                return NotFound();
            }

            return minutesAdded;
        }

        // PUT: api/MinutesAdded/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMinutesAdded(Guid id, MinutesAdded minutesAdded)
        {
            if (id != minutesAdded.Id)
            {
                return BadRequest();
            }

            _context.Entry(minutesAdded).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MinutesAddedExists(id))
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

        // POST: api/MinutesAdded
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MinutesAdded>> PostMinutesAdded(MinutesAdded minutesAdded)
        {
            _context.MinutesAddeds.Add(minutesAdded);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMinutesAdded", new { id = minutesAdded.Id }, minutesAdded);
        }

        // DELETE: api/MinutesAdded/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMinutesAdded(Guid id)
        {
            var minutesAdded = await _context.MinutesAddeds.FindAsync(id);
            if (minutesAdded == null)
            {
                return NotFound();
            }

            _context.MinutesAddeds.Remove(minutesAdded);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MinutesAddedExists(Guid id)
        {
            return _context.MinutesAddeds.Any(e => e.Id == id);
        }
    }
}
