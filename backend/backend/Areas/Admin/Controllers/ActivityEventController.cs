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
    public class ActivityEventController : Controller
    {
        private readonly AppDbContext _context;

        public ActivityEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ActivityEvent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ActivityEvents.Include(a => a.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ActivityEvent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEvent = await _context.ActivityEvents
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityEvent == null)
            {
                return NotFound();
            }

            return View(activityEvent);
        }

        // GET: Admin/ActivityEvent/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Admin/ActivityEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,BaseMinutes,MinutesPerTicket,StartTime,EndTime,WinnersVisible,PrizesVisible,Id")] ActivityEvent activityEvent)
        {
            if (ModelState.IsValid)
            {
                activityEvent.Id = Guid.NewGuid();
                _context.Add(activityEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", activityEvent.AppUserId);
            return View(activityEvent);
        }

        // GET: Admin/ActivityEvent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEvent = await _context.ActivityEvents.FindAsync(id);
            if (activityEvent == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", activityEvent.AppUserId);
            return View(activityEvent);
        }

        // POST: Admin/ActivityEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,BaseMinutes,MinutesPerTicket,StartTime,EndTime,WinnersVisible,PrizesVisible,Id")] ActivityEvent activityEvent)
        {
            if (id != activityEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityEventExists(activityEvent.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", activityEvent.AppUserId);
            return View(activityEvent);
        }

        // GET: Admin/ActivityEvent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEvent = await _context.ActivityEvents
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityEvent == null)
            {
                return NotFound();
            }

            return View(activityEvent);
        }

        // POST: Admin/ActivityEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activityEvent = await _context.ActivityEvents.FindAsync(id);
            if (activityEvent != null)
            {
                _context.ActivityEvents.Remove(activityEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityEventExists(Guid id)
        {
            return _context.ActivityEvents.Any(e => e.Id == id);
        }
    }
}
