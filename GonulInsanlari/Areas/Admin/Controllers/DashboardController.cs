using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("dashboard", Name = "dashboard")]
        public IActionResult Index()
        {

            return View();
        }

        [Route("visits")]
        public async Task<IActionResult> GetVisits()
        {








            using var c = new Context();


            var test2 = c.Visitors.GroupBy(x => x.Visited.Month);



            int count = c.Visitors
                     .Count(x => x.Visited.Month == DateTime.Now.Month);
            List<int> visits = new List<int>();
            visits.Add(count);
            visits.Add(97);
            visits.Add(76);
            visits.Add(54);
            visits.Add(34);
            visits.Add(48);
            visits.Add(15);

            return Json(visits);

        }

    }
}
