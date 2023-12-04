using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AssignmentBar : ViewComponent
    {
        UserManager<AppUser> userManager;
        AssignmentManager manager = new AssignmentManager(new EFAssignmentDAL());

        public AssignmentBar(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var assignments = manager.GetAssignmentsWithSender(user.Id);
            ViewBag.Count = "You have " + assignments.Count + " assignments";
            return View(assignments);
        }

    }
}
