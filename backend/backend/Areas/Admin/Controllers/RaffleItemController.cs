using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RaffleItemController : Controller
    {
        private readonly AppDbContext _context;

        public RaffleItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RaffleItem
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RaffleItems.Include(r => r.ActivityEvent);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/RaffleItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffleItem = await _context.RaffleItems
                .Include(r => r.ActivityEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raffleItem == null)
            {
                return NotFound();
            }

            return View(raffleItem);
        }

        // GET: Admin/RaffleItem/Create
        public IActionResult Create()
        {
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id");
            return View();
        }

        // POST: Admin/RaffleItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemName,ActivityEventId,Id")] RaffleItem raffleItem)
        {
            if (ModelState.IsValid)
            {
                raffleItem.Id = Guid.NewGuid();
                _context.Add(raffleItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id", raffleItem.ActivityEventId);
            return View(raffleItem);
        }

        // GET: Admin/RaffleItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffleItem = await _context.RaffleItems.FindAsync(id);
            if (raffleItem == null)
            {
                return NotFound();
            }
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id", raffleItem.ActivityEventId);
            return View(raffleItem);
        }

        // POST: Admin/RaffleItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemName,ActivityEventId,Id")] RaffleItem raffleItem)
        {
            if (id != raffleItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raffleItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaffleItemExists(raffleItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id", raffleItem.ActivityEventId);
            return View(raffleItem);
        }

        // GET: Admin/RaffleItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffleItem = await _context.RaffleItems
                .Include(r => r.ActivityEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raffleItem == null)
            {
                return NotFound();
            }

            return View(raffleItem);
        }

        // POST: Admin/RaffleItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var raffleItem = await _context.RaffleItems.FindAsync(id);
            if (raffleItem != null)
            {
                _context.RaffleItems.Remove(raffleItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaffleItemExists(Guid id)
        {
            return _context.RaffleItems.Any(e => e.Id == id);
        }
    }
}
