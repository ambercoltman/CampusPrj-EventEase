using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDBContexts _context;
        public VenuesController(ApplicationDBContexts context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View();
        }
    }
}
