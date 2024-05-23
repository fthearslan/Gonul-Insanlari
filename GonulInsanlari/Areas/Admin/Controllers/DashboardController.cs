using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    [Route("home")]
    public class DashboardController : Controller
    {
        UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Route("dashboard",Name ="dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
