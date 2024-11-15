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
        public IActionResult GetVisits()
        {



            using var c = new Context();

            List<Visitor> visitors = c.Visitors.Where(x => x.Visited.Year == DateTime.Now.Year).ToList();


            List<KeyValuePair<int, string>> mnths = new();
            visitors.ForEach(visitor => {


                mnths.Add(new(visitor.Visited.Month, visitor.Visited.ToString("MMMM")));


            });


            List<KeyValuePair<int, string>> months = mnths.Distinct().ToList();

            Dictionary<string, int> monthsAndcounts = new();

            months.ForEach(month =>
            {



                monthsAndcounts.Add(month.Value, visitors.Count(x => x.Visited.Month == month.Key));


            });


            return Json(new
            {

                months=monthsAndcounts.Keys,
                counts = monthsAndcounts.Values

            });

        }

        [Route("visits/year")]
        public IActionResult GetVisitsByYear()
        {


            using var c = new Context();

            List<Visitor> visitors = c.Visitors.Where(x => x.Visited.Year < DateTime.Now.Year).ToList();


            List<KeyValuePair<int, string>> yrs = new();
            visitors.ForEach(visitor => {


                yrs.Add(new(visitor.Visited.Year, visitor.Visited.ToString("yyyy")));


            });


            List<KeyValuePair<int, string>> years = yrs.Distinct().ToList();

            List<KeyValuePair<string, int>> yearsAndcounts = new();

            years.ForEach(year =>
            {



                yearsAndcounts.Add(new (year.Value, visitors.Count(x => x.Visited.Year == year.Key)));


            });


            return Json(yearsAndcounts.OrderByDescending(x=>x.Key).ToList());




        }
    }
}
