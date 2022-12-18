using Fiorello_Lab.DAL;
using Fiorello_Lab.Models;
using Fiorello_Lab.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_Lab.Controllers
{
    public class FlowerController : Controller
    {
        private readonly FioDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FlowerController(FioDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> AddToBasket(int flowerId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var flower = _context.Flowers.Include(x=> x.BasketItems).FirstOrDefault(x => x.Id == flowerId);
            if (flower == null)
                return NotFound();
            var basketItem =  _context.BasketItems.FirstOrDefault(x=> x.FlowerId == flower.Id && x.AppUserId == user.Id);
            if(basketItem == null)
            {
                //yenisini yarat
                basketItem = new BasketItem
                {
                    Id = 0,
                    FlowerId = flowerId,
                    AppUserId = user.Id,
                    Count = 1,
                    CreatedAt = DateTime.UtcNow.AddHours(4)
                };
                flower.BasketItems.Add(basketItem);

            }
            else
            {
                basketItem.Count++;
            }

            _context.SaveChanges();


            BasketViewModel BasketVm = new BasketViewModel
            {
                BasketItems = _context.BasketItems.Include(x=> x.Flower).Where(x => x.AppUserId == user.Id).ToList(),
                
            };



            return PartialView("_BasketPartial", BasketVm);
        }

    }
}