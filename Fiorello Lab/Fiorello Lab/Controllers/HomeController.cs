using Fiorello_Lab.DAL;
using Fiorello_Lab.Models;
using Fiorello_Lab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiorello_Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly FioDbContext _context;

        public HomeController(FioDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Categories=_context.Categories.ToList(),
                Flowers=_context.Flowers.ToList(),
                Settings = _context.Settings.ToDictionary(x=>x.Key, x=> x.Value)
                
            };

            return View(vm);
        }

       
    }
}