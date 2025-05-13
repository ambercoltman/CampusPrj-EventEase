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
        //lists booking details
        public async Task<IActionResult>Index(string search)
        {
            var bookings = from b in _context.BookingDetail select b;
            //will show the bookings if a search term is given
            if (!string.IsNullOrEmpty(search))
            {
                bookings = bookings.Where(b => b.EventName.Contains(search));
            }
            return View(bookings.ToList());
        }
    }
}

////Haritha Computers & Technology, 2020.How To Implement Search Functionality in ASP.NET CORE MVC Index View VS2019. [video online]. Available at:<https://www.youtube.com/watch?v=JHrBbQAqm0s> [Accessed 11 May 2025].