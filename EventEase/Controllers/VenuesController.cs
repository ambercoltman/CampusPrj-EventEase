using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDBContexts _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        public VenuesController(ApplicationDBContexts context, IConfiguration config)
        {
            _context = context;
            _blobServiceClient = new BlobServiceClient(config["AzureBlobStorage:ConnectionStrings"]);
            _containerName = config["AzureBlobStorage:ContainerName"];
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
        public async Task<IActionResult> Create(IFormFile imageFile, string venueName, string location, int capacity)
        {
            if (imageFile == null) return BadRequest("No image has been uploaded. Try again."); 
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blob = container.GetBlobClient(imageFile.FileName);

            using var stream = imageFile.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);

            var existingVenue = _context.Venues.Where(p => p.VenueName.StartsWith(venueName)).ToList();
            string uniqueName = existingVenue.Count > 0 ? $"{venueName}{existingVenue.Count + 1}" : venueName;

            var venues = new Venues
            {
                VenueName = uniqueName,
                Location = location,
                Capacity = capacity,
                ImageUrl = blob.Uri.ToString()
            };
            _context.Venues.Add(venues);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
            var booking = await _context.Booking.FindAsync(id);
            var venues = await _context.Venues.FindAsync(id);
            if (venues == null)
            {
                return NotFound();
            }

            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobUri = new Uri(venues.ImageUrl); 
            var blobName = WebUtility.UrlDecode(blobUri.Segments.Last());

            var blob = container.GetBlobClient(blobName);

            if (_context.Venues.Count(v => v.ImageUrl == venues.ImageUrl) <= 1)
            {
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }

            //Microsoft (2024) demonstrates how to use the (.Any) and how to use ModelState.AddModelError

            if (_context.Booking.Any(b => b.VenuesId == id))
            {
                ModelState.AddModelError("", "Can not delete because has active booking.");
                return RedirectToAction(nameof(Index));
            }

            _context.Venues.Remove(_context.Venues.Find(id));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

////IIEVC School of Computer Science, 2025. CLDV6211: Mastering SQL, Cloud & Azure(2025 Guide). [video online]. Available at: <https://www.youtube.com/playlist?list=PL480DYS-b_kevhFsiTpPIB2RzhKPig4iK> [Accessed 06 April 2025].
////IIEVC School of Computer Science, 2025. All about the blob storage part 2 with EF!! - CLDV6211. [video online]. Available at: <https://www.youtube.com/watch?v=lQ7VGslAVrU&t=131s> [Accessed 08 May 2025].
///Microsoft. 2024.Model validation in ASP.NET Core MVC (Version 2.0) [Source code]. Available at: <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-9.0> [Accessed 12 May 2025].
