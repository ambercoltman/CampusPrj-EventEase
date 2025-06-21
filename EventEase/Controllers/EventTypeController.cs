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
            var eventType = await _context.EventType.ToListAsync();
            return View(eventType);
        }
    }
}
