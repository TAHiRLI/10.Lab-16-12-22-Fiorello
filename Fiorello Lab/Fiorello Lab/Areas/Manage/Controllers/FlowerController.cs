using Fiorello_Lab.DAL;
using Fiorello_Lab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using Pustok.Helpers;
using System.Collections.Immutable;
using System.Composition;
using System.Data;

namespace Fiorello_Lab.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class FlowerController : Controller
    {
        private readonly FioDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FlowerController(FioDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Flowers.Include(x => x.Categorie).Include(x => x.FlowerImages).ToList();
            return View(model);

        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Flower flower)
        {

            if (flower.MainImgFile == null)
                ModelState.AddModelError("MainImgFile", "Flower Image Is Required");
            if (flower.MainImgFile != null && flower.MainImgFile.ContentType != "image/jpeg" && flower.MainImgFile.ContentType != "image/png")
                ModelState.AddModelError("MainImgFile", "Image type must be Jpeg or png");
            if (flower.MainImgFile != null && flower.MainImgFile.Length > 190157)
                ModelState.AddModelError("MainImgFile", "Image type must smaller than 2MB");


            if (flower.FlowerImgFiles != null)
            {

                foreach (var file in flower.FlowerImgFiles)
                {
                    if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                        ModelState.AddModelError("FlowerImgFiles", "Image type must be Jpeg or png");
                    if (file.Length > 190157)
                        ModelState.AddModelError("FlowerImgFiles", "Image type must smaller than 2MB");

                }

            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View();
            }


            FlowerImage flowerImage = new FlowerImage
            {
                ImgUrl = FileManager.Save(flower.MainImgFile, _env.WebRootPath, "Uploads/Flowers", 100),
                IsMain = true

            };
            flower.FlowerImages.Add(flowerImage);


            // other images
            if (flower.FlowerImgFiles != null)
            {

                foreach (var file in flower.FlowerImgFiles)
                {
                    FlowerImage flowerImages = new FlowerImage
                    {
                        ImgUrl = FileManager.Save(file, _env.WebRootPath, "Uploads/Flowers", 100),
                        IsMain = false

                    };
                    flower.FlowerImages.Add(flowerImages);
                }
            }



            _context.Add(flower);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Flowers.Include(x => x.FlowerImages).FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = _context.Categories.ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Flower flower)
        { 
            CheckFiles(flower.FlowerImgFiles, flower.MainImgFile);

            Flower existingFlower = _context.Flowers.Include(x=> x.Categorie).FirstOrDefault(x => x.Id == flower.Id);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();

                return View(existingFlower);
            }

            List<int> removedIds = _context.FlowerImages.Where(x=> x.FlowerId == flower.Id && x.IsMain == false && !flower.FlowerImageIds.Contains(x.Id)).Select(x=>x.Id).ToList();

            foreach (var id in removedIds)
            {
                FlowerImage fl = _context.FlowerImages.FirstOrDefault(x => x.Id == id);
                FileManager.Delete(_env.WebRootPath, "Uploads/Flowers", fl.ImgUrl);
                _context.FlowerImages.Remove(fl);
            }
            if(flower.MainImgFile != null)
            {
                FlowerImage fl = _context.FlowerImages.FirstOrDefault(x => x.FlowerId == flower.Id && x.IsMain == true);
                FileManager.Delete(_env.WebRootPath, "Uploads/Flowers", fl.ImgUrl);
                _context.FlowerImages.Remove(fl);
                FlowerImage flowerImage = new FlowerImage
                {
                    FlowerId= flower.Id,
                    ImgUrl = FileManager.Save(flower.MainImgFile, _env.WebRootPath, "Uploads/Flowers", 100),
                    IsMain = true

                };
                _context.FlowerImages.Add(flowerImage);
            }

            if (flower.FlowerImgFiles != null)
            {

                foreach (var file in flower.FlowerImgFiles)
                {
                    FlowerImage flowerImages = new FlowerImage
                    {
                        FlowerId = flower.Id,
                        ImgUrl = FileManager.Save(file, _env.WebRootPath, "Uploads/Flowers", 100),
                        IsMain = false

                    };
                    _context.FlowerImages.Add(flowerImages);
                }
            }





            _context.SaveChanges();



            return RedirectToAction("Index");
        }

        private void CheckFiles(List<IFormFile> items, IFormFile mainImage)
        {
            if (mainImage != null)
            {

                if (mainImage.ContentType != "image/jpeg" && mainImage.ContentType != "image/png")
                    ModelState.AddModelError("MainImgFile", "File type must be jpeg, jpg or png");
                if (mainImage.Length > 190152)
                    ModelState.AddModelError("MainImgFile", "File size should be less than 2Mb");
            }

            if (items != null)
            {

                foreach (var file in items)
                {
                    if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                        ModelState.AddModelError("FlowerImages", "File type must be jpeg, jpg or png");
                    if (file.Length > 190152)
                        ModelState.AddModelError("FlowerImages", "File size should be less than 2Mb");
                }
            }
        }
    }
}
