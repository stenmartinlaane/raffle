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
    public class ParticipantEventController : Controller
    {
        private readonly AppDbContext _context;

        public ParticipantEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ParticipantEvent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ParticipantEvents.Include(p => p.ActivityEvent).Include(p => p.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ParticipantEvent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantEvent = await _context.ParticipantEvents
                .Include(p => p.ActivityEvent)
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantEvent == null)
            {
                return NotFound();
            }

            return View(participantEvent);
        }

        // GET: Admin/ParticipantEvent/Create
        public IActionResult Create()
        {
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id");
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Admin/ParticipantEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,Anonymous,ActivityEventId,Id")] ParticipantEvent participantEvent)
        {
            if (ModelState.IsValid)
            {
                participantEvent.Id = Guid.NewGuid();
                _context.Add(participantEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id", participantEvent.ActivityEventId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", participantEvent.AppUserId);
            return View(participantEvent);
        }

        // GET: Admin/ParticipantEvent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantEvent = await _context.ParticipantEvents.FindAsync(id);
            if (participantEvent == null)
            {
                return NotFound();
            }
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id", participantEvent.ActivityEventId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", participantEvent.AppUserId);
            return View(participantEvent);
        }

        // POST: Admin/ParticipantEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Anonymous,ActivityEventId,Id")] ParticipantEvent participantEvent)
        {
            if (id != participantEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participantEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantEventExists(participantEvent.Id))
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
            ViewData["ActivityEventId"] = new SelectList(_context.ActivityEvents, "Id", "Id", participantEvent.ActivityEventId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", participantEvent.AppUserId);
            return View(participantEvent);
        }

        // GET: Admin/ParticipantEvent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantEvent = await _context.ParticipantEvents
                .Include(p => p.ActivityEvent)
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantEvent == null)
            {
                return NotFound();
            }

            return View(participantEvent);
        }

        // POST: Admin/ParticipantEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var participantEvent = await _context.ParticipantEvents.FindAsync(id);
            if (participantEvent != null)
            {
                _context.ParticipantEvents.Remove(participantEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantEventExists(Guid id)
        {
            return _context.ParticipantEvents.Any(e => e.Id == id);
        }
    }
}
