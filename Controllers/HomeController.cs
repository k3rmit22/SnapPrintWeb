using Microsoft.AspNetCore.Mvc;
using SnapPrintWeb.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SnapPrintWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult index ()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: /Home/Upload
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Save the file or process it
                // Example: Save to the server
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Redirect to the Index after upload
                return RedirectToAction("Index");
            }

            // Return to the Index with an error message if no file was uploaded
            ModelState.AddModelError("", "Please select a file.");
            return View("Index");
        }

    }
}
