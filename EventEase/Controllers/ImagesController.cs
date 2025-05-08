using Azure.Storage.Blobs;
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

    }
}
