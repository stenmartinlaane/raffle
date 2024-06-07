using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RaffleItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RaffleItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RaffleItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaffleItem>>> GetRaffleItems()
        {
            return await _context.RaffleItems.ToListAsync();
        }

        // GET: api/RaffleItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RaffleItem>> GetRaffleItem(Guid id)
        {
            var raffleItem = await _context.RaffleItems.FindAsync(id);

            if (raffleItem == null)
            {
                return NotFound();
            }

            return raffleItem;
        }

        // PUT: api/RaffleItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaffleItem(Guid id, RaffleItem raffleItem)
        {
            if (id != raffleItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(raffleItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaffleItemExists(id))
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

        // POST: api/RaffleItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RaffleItem>> PostRaffleItem(RaffleItem raffleItem)
        {
            _context.RaffleItems.Add(raffleItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRaffleItem", new { id = raffleItem.Id }, raffleItem);
        }

        // DELETE: api/RaffleItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaffleItem(Guid id)
        {
            var raffleItem = await _context.RaffleItems.FindAsync(id);
            if (raffleItem == null)
            {
                return NotFound();
            }

            _context.RaffleItems.Remove(raffleItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RaffleItemExists(Guid id)
        {
            return _context.RaffleItems.Any(e => e.Id == id);
        }
    }
}
