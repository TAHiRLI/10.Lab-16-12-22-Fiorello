using Fiorello_Lab.Models;
using Fiorello_Lab.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace Fiorello_Lab.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(await _userManager.FindByNameAsync(registerVM.Username) !=null )
            {
                ModelState.AddModelError("Username", "User already Exists");
                return View();
            }

            AppUser appUser = new AppUser
            {
                Fullname = registerVM.Fullname,
                UserName = registerVM.Username
            };
            var result =await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "Member");
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        //public async Task< IActionResult > CreateRoles()
        //{
        //    IdentityRole role1 = new IdentityRole("Member");
        //    IdentityRole role2 = new IdentityRole("SuperAdmin");
        //   await _roleManager.CreateAsync(role2);
        //   await _roleManager.CreateAsync(role1);

        //    return Ok();
        //}
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
            
            
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult isLoggedIn()
        {
            return Content(User.Identity.Name);
        }
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
