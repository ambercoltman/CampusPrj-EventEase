using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingDetailController : Controller
    {
        private readonly ApplicationDBContexts _context;
        public BookingDetailController(ApplicationDBContexts context)
        {
            _context = context;
        }
        public async Task<IActionResult>Index(string search)
        {
            ViewData["BookingDetails"] = search;
            var query = from d in _context.BookingDetail select d;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d => d.EventName.Contains(search));
            }
            return View(await query.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> BookingDetails(string search)
        {
            var bookings = from b in _context.BookingDetail select b;
            if(!string.IsNullOrEmpty(search))
            {
                bookings = bookings.Where(b => b.EventName.Contains(search));
            }
            return View(bookings.ToList());
        }
    }
}
