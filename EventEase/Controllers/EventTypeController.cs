using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly ApplicationDBContexts _context;
        public EventTypeController(ApplicationDBContexts context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var eventT = await _context.EventType.ToListAsync();
            return View(eventT);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventType eventT)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventT);

        }

        public async Task<IActionResult> Details(int? id)
        {
            var eventsT = await _context.EventType.FirstOrDefaultAsync(mbox => mbox.EventTypeID == id);

            if (eventsT == null)
            {
                return NotFound();
            }
            return View(eventsT);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventT = await _context.EventType.FindAsync(id);
            if (eventT == null)
            {
                return NotFound();
            }
            return View(eventT);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var eventT = await _context.EventType.FirstOrDefaultAsync(m => m.EventTypeID == id);

            if (eventT == null)
            {
                return NotFound();
            }
            return View(eventT);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var eventT = await _context.EventType.FindAsync(id);
            if (eventT == null)
            {
                return NotFound();
            }

            _context.EventType.Remove(eventT);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
