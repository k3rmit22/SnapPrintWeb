using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SnapPrintWeb.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "No file uploaded.";
                return RedirectToAction("Index");
            }

            // Validate file type
            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
            {
                TempData["ErrorMessage"] = "Only PDF files are allowed.";
                return RedirectToAction("Index");
            }

            // Validate file size (e.g., max 10MB)
            if (file.Length > 10 * 1024 * 1024)
            {
                TempData["ErrorMessage"] = "File size must not exceed 10MB.";
                return RedirectToAction("Index");
            }

            // Ensure upload directory exists
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Save file
            var filePath = Path.Combine(uploadDir, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            TempData["SuccessMessage"] = "File uploaded successfully!";
            return RedirectToAction("Index");
        }
    }
}
