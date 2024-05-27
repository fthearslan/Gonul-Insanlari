using DataAccessLayer.Concrete.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Admin
{
    public class AdminProfileLastActivities : ViewComponent
    {

        public Context _context;

        public AdminProfileLastActivities(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int userId)
        {
            List<(string, string)> model = new List<(string, string)>();

            var userLogins = _context.UsersLogins.
                Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Date)
                .Select(x => new
                {
                    x.Description,
                    x.Type
                })
                .Take(5)
                .AsNoTrackingWithIdentityResolution()
                .ToList();

            userLogins.ForEach(x => model.Add(new(x.Description, x.Type.ToString())));

            

            return View(model);
        }


    }



}
