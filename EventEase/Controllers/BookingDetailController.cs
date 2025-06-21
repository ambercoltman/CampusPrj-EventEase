using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<IActionResult>Index(string search, string searchBy)
        {
            var bookings = from b in _context.BookingDetail select b;
            if(searchBy == "TypeName")
            {
                return View(bookings.Where(x => x.EventType == search || search == null).ToList());
            }
            else if (searchBy == "isAvailable")
            {
                
                if(bool.TryParse(search, out bool value))
                { 
                return View(bookings.Where(x => x.isAvailable == value || search == null).ToList());
                }
                else
                {
                    return View(bookings.ToList());
                }
            }
            else
            {
                return View(bookings.Where(x => x.EventName.StartsWith(search) || search == null).ToList());
            }

        }
    }
}

////Haritha Computers & Technology, 2020.How To Implement Search Functionality in ASP.NET CORE MVC Index View VS2019. [video online]. Available at:<https://www.youtube.com/watch?v=JHrBbQAqm0s> [Accessed 11 May 2025].