using System.Net;
using App.DAL.EF;
using App.Domain;
using AutoMapper;
using backend.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ActivityEventController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly backend.Helpers.PublicDTODomainMapper<App.DTO.v1_0.ActivityEvent, App.Domain.ActivityEvent> _mapper;

        public ActivityEventController(AppDbContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = new backend.Helpers.PublicDTODomainMapper<App.DTO.v1_0.ActivityEvent, App.Domain.ActivityEvent>(autoMapper);
        }

        // GET: api/ActivityEvent
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.ActivityEvent>>((int) HttpStatusCode.OK)]
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.ActivityEvent>>> GetActivityEvents()
        {
            var res = (await _context.ActivityEvents.ToListAsync()).Select(ae => _mapper.Map(ae));
            return Ok(res);
        }

        // GET: api/ActivityEvent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1_0.ActivityEvent>> GetActivityEvent(Guid id)
        {
            var activityEvent = await _context.ActivityEvents.FindAsync(id);

            if (activityEvent == null)
            {
                return NotFound();
            }

            return _mapper.Map(activityEvent);
        }

        // PUT: api/ActivityEvent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityEvent(Guid id, App.DTO.v1_0.ActivityEvent activityEvent)
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
        [Authorize(Policy = "id_policy")]
        [HttpPost]
        public async Task<ActionResult<ActivityEvent>> PostActivityEvent(App.DTO.v1_0.ActivityEvent activityEvent)
        {
            var domain = _mapper.Map(activityEvent)!;
            domain.AppUserId = User.GetUserId();
            domain.Id = new Guid();
            var res = await _context.ActivityEvents.AddAsync(domain);
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
