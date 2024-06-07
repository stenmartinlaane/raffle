using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParticipantEventController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipantEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ParticipantEvent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantEvent>>> GetParticipantEvents()
        {
            return await _context.ParticipantEvents.ToListAsync();
        }

        // GET: api/ParticipantEvent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantEvent>> GetParticipantEvent(Guid id)
        {
            var participantEvent = await _context.ParticipantEvents.FindAsync(id);

            if (participantEvent == null)
            {
                return NotFound();
            }

            return participantEvent;
        }

        // PUT: api/ParticipantEvent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipantEvent(Guid id, ParticipantEvent participantEvent)
        {
            if (id != participantEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(participantEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantEventExists(id))
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

        // POST: api/ParticipantEvent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipantEvent>> PostParticipantEvent(ParticipantEvent participantEvent)
        {
            _context.ParticipantEvents.Add(participantEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipantEvent", new { id = participantEvent.Id }, participantEvent);
        }

        // DELETE: api/ParticipantEvent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipantEvent(Guid id)
        {
            var participantEvent = await _context.ParticipantEvents.FindAsync(id);
            if (participantEvent == null)
            {
                return NotFound();
            }

            _context.ParticipantEvents.Remove(participantEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantEventExists(Guid id)
        {
            return _context.ParticipantEvents.Any(e => e.Id == id);
        }
    }
}
