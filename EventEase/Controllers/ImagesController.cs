using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ApplicationDBContexts _contexts;
        public readonly BlobServiceClient _blobServiceClient;
        public readonly string _containerName;

        public ImagesController(ApplicationDBContexts contexts, IConfiguration config)
        {
            _contexts = contexts;
            _blobServiceClient = new BlobServiceClient(config["AzureBlobStorage:ConnectionStrings"]);
            _containerName = config["AzureBlobStorage:ContainerName"];
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contexts.Venues.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile imageFile, string VenueName, string Location, int Capacity)
        {
            if (imageFile == null) return BadRequest("No image has been uploaded. Try again."); //image not uploading(fix)

            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blob = container.GetBlobClient(imageFile.FileName);

            using var stream = imageFile.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);

            var existingVenues = _contexts.Venues.Where(p => p.VenueName.StartsWith(VenueName)).ToList();
            string uniqueName = existingVenues.Count > 0 ? $"{VenueName}{existingVenues.Count + 1}" : VenueName;

            var Venues = new Venues { VenueName = uniqueName, Location = Location, Capacity = Capacity, ImageUrl = blob.Uri.ToString() };
            _contexts.Venues.Add(Venues);
            await _contexts.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Venues = await _contexts.Venues.FindAsync(id);
            if (Venues == null) return NotFound();

            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobUri = new Uri(Venues.ImageUrl); //when try to delete, shows this is the problem (fix)
            var blobName = WebUtility.UrlDecode(blobUri.Segments.Last()); 

            var blob = container.GetBlobClient(blobName);

            if (_contexts.Venues.Count(pet => pet.ImageUrl == pet.ImageUrl) <= 1)
            {
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }

            _contexts.Venues.Remove(Venues);
            await _contexts.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
