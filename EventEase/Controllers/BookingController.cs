using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDBContexts _context;
        public BookingController(ApplicationDBContexts context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Booking = await _context.Booking.ToListAsync();
            return View(Booking);
        }
    
        public async Task <IActionResult> Create()
        {
            ViewBag.Venues = _context.Venues.ToList();
            ViewBag.Events = _context.Events.ToList();
            return View();
        }
        [HttpPost]

        public async Task <IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _context.Venues.ToList();
            ViewBag.Companies = _context.Events.ToList();
            return View(booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(mbox => mbox.Id == id);

            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
