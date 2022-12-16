using Fiorello_Lab.DAL;
using Fiorello_Lab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_Lab.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly FioDbContext _context;

        public CategoryController(FioDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Categories.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Categorie category = new Categorie
            {
                Name = categorie.Name
            };
            _context.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existingCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if(existingCategory != null)
            {
            _context.Categories.Remove(existingCategory);
            _context.SaveChanges();

            }

            return RedirectToAction("Index");

        }

    }
}
