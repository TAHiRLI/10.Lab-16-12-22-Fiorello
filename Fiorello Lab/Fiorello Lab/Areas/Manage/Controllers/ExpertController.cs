using Fiorello_Lab.DAL;
using Fiorello_Lab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.Helpers;
using System.Data;

namespace Fiorello_Lab.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class ExpertController : Controller
    {
        private readonly FioDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ExpertController(FioDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
          var model =   _context.Experts.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Expert expert)
        {
            if (expert.ExpertFile == null)
                ModelState.AddModelError("ExpertFile", "Expert Image Is Required");
            if (expert.ExpertFile != null && expert.ExpertFile.ContentType != "image/jpeg" && expert.ExpertFile.ContentType != "image/png")
                ModelState.AddModelError("ExpertFile", "Image type must be Jpeg or png");
            if (expert.ExpertFile != null && expert.ExpertFile.Length > 190157)
                ModelState.AddModelError("ExpertFile", "Image type must smaller than 2MB");

            if (!ModelState.IsValid)
            {
                return View();
            }


           string newFileName =   FileManager.Save(expert.ExpertFile, _env.WebRootPath, "Uploads/Experts", 100);

            expert.Img = newFileName;

            _context.Experts.Add(expert);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
           var existingExpert =  _context.Experts.FirstOrDefault(x => x.Id == id);
            
                FileManager.Delete(_env.WebRootPath, "Uploads/Experts", existingExpert.Img);
            _context.Experts.Remove(existingExpert);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
