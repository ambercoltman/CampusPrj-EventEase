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
            //Netcode-Hub shows what the if statements would look like
            if(searchBy == "TypeName")
            {
                return View(bookings.Where(x => x.EventType == search || search == null).ToList());
            }
            else if (searchBy == "isAvailable")
            {
                //HaloFour demontrates how to use a tryparse
                if (bool.TryParse(search, out bool value))
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
///Netcode-Hub, 2022. ASP.NET MVC Tutorials - Implementing Search Functionality in ASP>NET MVC. [video online] 26 October. Available at: <https://www.youtube.com/watch?v=bzWJOxBR-MY> [Accessed 21 June 2025].
///HaloFour, 2017. Proposal: Support out/pattern variables in LINQ with query-scoping. [onine] 21 February. Available at: <https://github.com/dotnet/csharplang/discussions/159?utm> [Accessed 21 June 2025].