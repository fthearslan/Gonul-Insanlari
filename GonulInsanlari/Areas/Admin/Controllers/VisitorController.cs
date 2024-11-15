using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GonulInsanlari.Areas.Admin.Controllers
{


    [AllowAnonymous]
    [Route("visitors")]
    public class VisitorController : Controller
    {
        

        [Route("visits")]
        public IActionResult GetVisits()
        {
            using Context c = new Context();

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

                months = monthsAndcounts.Keys,
                counts = monthsAndcounts.Values

            });

        }

        [Route("visits/year")]
        public IActionResult GetVisitsByYear()
        {

            using Context c = new Context();

            List<Visitor> visitors = c.Visitors.Where(x => x.Visited.Year < DateTime.Now.Year).ToList();


            List<KeyValuePair<int, string>> yrs = new();
            visitors.ForEach(visitor => {


                yrs.Add(new(visitor.Visited.Year, visitor.Visited.ToString("yyyy")));


            });


            List<KeyValuePair<int, string>> years = yrs.Distinct().ToList();

            List<KeyValuePair<string, int>> yearsAndcounts = new();

            years.ForEach(year =>
            {



                yearsAndcounts.Add(new(year.Value, visitors.Count(x => x.Visited.Year == year.Key)));


            });


            return Json(yearsAndcounts.OrderByDescending(x => x.Key).ToList());




        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteRecords()
        {

            using Context context = new Context();

            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Visitors");


            return Ok();

        }

    }
}
