using Fiorello_Lab.Models;
using Fiorello_Lab.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_Lab.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        //public async Task<IActionResult> Index()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        Fullname= "tahir",
        //        UserName = "TahirAdmin"
        //    };
        //   await  _userManager.CreateAsync(appUser, "Admin123");
        //    await _userManager.AddToRoleAsync(appUser,"SuperAdmin");

        //    return Ok();
        //}

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(adminLoginViewModel loginVm)
        {

            if (!ModelState.IsValid)
                return View();
            var user = await _userManager.FindByNameAsync(loginVm.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many attempts please try after 5 minutes");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "username or passoword is incorrect");
                return View();
            }


            return RedirectToAction("Index", "dashboard");
        }
    }
}
