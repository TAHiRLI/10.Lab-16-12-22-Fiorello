using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_Lab.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
