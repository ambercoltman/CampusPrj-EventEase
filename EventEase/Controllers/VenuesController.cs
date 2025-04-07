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

            var Venues = await _context.Venues.ToListAsync();
            return View(Venues);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venues venues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venues);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var venues = await _context.Venues.FirstOrDefaultAsync(mbox => mbox.Id == id);

            if (venues == null)
            {
                return NotFound();
            }
            return View(venues);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues.FindAsync(id);
            if (venues == null)
            {
                return NotFound();
            }
            return View(venues);
        }

        public bool VenuesExists(int id)
        {
            return _context.Venues.Any(e => e.Id == id);
        }
        [HttpPost]

        public async Task <IActionResult> Edit(int id,Venues venues)
        {
            if(id != venues.Id)
            {
                return NotFound();
            }
            
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(venues);
                        await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!VenuesExists(venues.Id))
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
            return View(venues);
        }

        public async Task <IActionResult> Delete(int? id)
        {
            var venues = await _context.Venues.FirstOrDefaultAsync(m => m.Id == id); 

            if (venues == null)
            { 
                return NotFound();
            }
            return View(venues);
        }
        [HttpPost]

        public async Task <IActionResult> Delete(int id)
        {
            var venues = await _context.Venues.FindAsync(id);
            if(venues == null)
            {
                return NotFound();
            }

            _context.Venues.Remove(venues);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

//IIEVC School of Computer Science, 2025. CLDV6211: Mastering SQL, Cloud & Azure(2025 Guide). [video online]. Available at: <https://www.youtube.com/playlist?list=PL480DYS-b_kevhFsiTpPIB2RzhKPig4iK> [Accessed 06 April 2025].
