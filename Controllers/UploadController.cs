using Microsoft.AspNetCore.Mvc;
using SnapPrintWeb.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SnapPrintWeb.Controllers
{
    public class UploadController : Controller
    {
        private readonly SnapPrintDbContext _context;

        public UploadController(SnapPrintDbContext context)
        {
            _context = context;
        }

        // Render the file upload form
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


            // Read the file content into a byte array
            byte[] fileData;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();
            }

            // Create a new UploadedFile object and save to the database
            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                UploadedDateTime = DateTime.Now,
                FileData = fileData
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "File uploaded successfully!";
            return RedirectToAction("Index");
        }
    }
}
