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
        public async Task<IActionResult> Create(IFormFile imageFile, string VenueName, string Location, int Capacity)
        {
            if (imageFile == null) return BadRequest("No image has been uploaded. Try again."); //image not uploading(fix)

            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blob = container.GetBlobClient(imageFile.FileName);

            using var stream = imageFile.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);

            var existingVenues = _context.Venues.Where(p => p.VenueName.StartsWith(VenueName)).ToList();
            string uniqueName = existingVenues.Count > 0 ? $"{VenueName}{existingVenues.Count + 1}" : VenueName;

            var venues = new Venues
            {
                VenueName = uniqueName,
                Location = Location,
                Capacity = Capacity,
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
            var venues = await _context.Venues.FindAsync(id);
            if (venues == null) return NotFound();

            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobUri = new Uri(venues.ImageUrl); //when try to delete, shows this is the problem (fix)
            var blobName = WebUtility.UrlDecode(blobUri.Segments.Last());

            var blob = container.GetBlobClient(blobName);

            if (_context.Venues.Count(Venues => Venues.ImageUrl == Venues.ImageUrl) <= 1)
            {
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }

            _context.Venues.Remove(venues);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

////IIEVC School of Computer Science, 2025. CLDV6211: Mastering SQL, Cloud & Azure(2025 Guide). [video online]. Available at: <https://www.youtube.com/playlist?list=PL480DYS-b_kevhFsiTpPIB2RzhKPig4iK> [Accessed 06 April 2025].
