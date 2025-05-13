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
        //listing all the bookings
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
            //Microsoft (2024) demonstrates how to use the (.Any)

            //checks if there is any booking that has the same VenueId and BookingDate
            bool existingBooking = _context.Booking.Any(b => b.VenuesId == booking.VenuesId
                                                              && b.bookingDate == booking.bookingDate);

            if (existingBooking != null)
            {
                //this error message will print out  if a booking exis
                ModelState.AddModelError("","Venue is already booked for that specific date and time");
                ViewBag.Students = _context.Venues.ToList();
                ViewBag.Companies = _context.Events.ToList();
                return View(booking);
            }
        
            //booking will be created if there isnt an existing booking
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        //to check if booking exists
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
                catch (Exception ex)
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

////IIEVC School of Computer Science, 2025. CLDV6211: Mastering SQL, Cloud & Azure(2025 Guide). [video online]. Available at: <https://www.youtube.com/playlist?list=PL480DYS-b_kevhFsiTpPIB2RzhKPig4iK> [Accessed 06 April 2025].
///Microsoft. 2024.Model validation in ASP.NET Core MVC (Version 2.0) [Source code]. Available at: <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-9.0> [Accessed 12 May 2025].
