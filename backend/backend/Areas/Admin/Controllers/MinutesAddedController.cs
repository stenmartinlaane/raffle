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
    public class MinutesAddedController : Controller
    {
        private readonly AppDbContext _context;

        public MinutesAddedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MinutesAdded
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MinutesAddeds.Include(m => m.ParticipantEvent);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/MinutesAdded/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var minutesAdded = await _context.MinutesAddeds
                .Include(m => m.ParticipantEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (minutesAdded == null)
            {
                return NotFound();
            }

            return View(minutesAdded);
        }

        // GET: Admin/MinutesAdded/Create
        public IActionResult Create()
        {
            ViewData["ParticipantEventId"] = new SelectList(_context.ParticipantEvents, "Id", "Id");
            return View();
        }

        // POST: Admin/MinutesAdded/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Amount,ParticipantEventId,Id")] MinutesAdded minutesAdded)
        {
            if (ModelState.IsValid)
            {
                minutesAdded.Id = Guid.NewGuid();
                _context.Add(minutesAdded);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParticipantEventId"] = new SelectList(_context.ParticipantEvents, "Id", "Id", minutesAdded.ParticipantEventId);
            return View(minutesAdded);
        }

        // GET: Admin/MinutesAdded/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var minutesAdded = await _context.MinutesAddeds.FindAsync(id);
            if (minutesAdded == null)
            {
                return NotFound();
            }
            ViewData["ParticipantEventId"] = new SelectList(_context.ParticipantEvents, "Id", "Id", minutesAdded.ParticipantEventId);
            return View(minutesAdded);
        }

        // POST: Admin/MinutesAdded/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Date,Amount,ParticipantEventId,Id")] MinutesAdded minutesAdded)
        {
            if (id != minutesAdded.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(minutesAdded);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MinutesAddedExists(minutesAdded.Id))
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
            ViewData["ParticipantEventId"] = new SelectList(_context.ParticipantEvents, "Id", "Id", minutesAdded.ParticipantEventId);
            return View(minutesAdded);
        }

        // GET: Admin/MinutesAdded/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var minutesAdded = await _context.MinutesAddeds
                .Include(m => m.ParticipantEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (minutesAdded == null)
            {
                return NotFound();
            }

            return View(minutesAdded);
        }

        // POST: Admin/MinutesAdded/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var minutesAdded = await _context.MinutesAddeds.FindAsync(id);
            if (minutesAdded != null)
            {
                _context.MinutesAddeds.Remove(minutesAdded);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MinutesAddedExists(Guid id)
        {
            return _context.MinutesAddeds.Any(e => e.Id == id);
        }
    }
}
