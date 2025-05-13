using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDBContexts _context;
        public EventsController(ApplicationDBContexts context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Events = await _context.Events.ToListAsync();
            return View(Events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Events events)
        {
            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);

        }

        public async Task<IActionResult> Details(int? id)
        {
            var events = await _context.Events.FirstOrDefaultAsync(mbox => mbox.Id == id);

            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        public bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, Events events)
        {
            if (id != events.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.Id))
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
            return View(events);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var events = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Booking.FindAsync(id);

            //Microsoft (2024) demonstrates how to use the (.Any) and how to use ModelState.AddModelError

            if (_context.Booking.Any(b => b.EventsId == id))
            {
                ModelState.AddModelError("","Can not delete because has active booking.");
                return RedirectToAction(nameof(Index));
            }

            _context.Events.Remove(_context.Events.Find(id));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

    }
}

////IIEVC School of Computer Science, 2025. CLDV6211: Mastering SQL, Cloud & Azure(2025 Guide). [video online]. Available at: <https://www.youtube.com/playlist?list=PL480DYS-b_kevhFsiTpPIB2RzhKPig4iK> [Accessed 06 April 2025].
///Microsoft. 2024.Model validation in ASP.NET Core MVC (Version 2.0) [Source code]. Available at: <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-9.0> [Accessed 12 May 2025].
