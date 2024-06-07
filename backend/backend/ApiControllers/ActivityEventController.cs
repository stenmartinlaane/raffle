using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ActivityEventController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivityEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ActivityEvent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityEvent>>> GetActivityEvents()
        {
            return await _context.ActivityEvents.ToListAsync();
        }

        // GET: api/ActivityEvent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityEvent>> GetActivityEvent(Guid id)
        {
            var activityEvent = await _context.ActivityEvents.FindAsync(id);

            if (activityEvent == null)
            {
                return NotFound();
            }

            return activityEvent;
        }

        // PUT: api/ActivityEvent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityEvent(Guid id, ActivityEvent activityEvent)
        {
            if (id != activityEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(activityEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityEventExists(id))
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

        // POST: api/ActivityEvent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityEvent>> PostActivityEvent(ActivityEvent activityEvent)
        {
            _context.ActivityEvents.Add(activityEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityEvent", new { id = activityEvent.Id }, activityEvent);
        }

        // DELETE: api/ActivityEvent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityEvent(Guid id)
        {
            var activityEvent = await _context.ActivityEvents.FindAsync(id);
            if (activityEvent == null)
            {
                return NotFound();
            }

            _context.ActivityEvents.Remove(activityEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityEventExists(Guid id)
        {
            return _context.ActivityEvents.Any(e => e.Id == id);
        }
    }
}
