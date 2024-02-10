using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AssignmentBar : ViewComponent
    {
        UserManager<AppUser> userManager;
        private readonly IAssignmentService _manager;

        public AssignmentBar(UserManager<AppUser> UserManager, IAssignmentService manager)
        {
            userManager = UserManager;
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var assignments = _manager.GetAssignmentsWithSender(user.Id);
            ViewBag.Count = "You have " + assignments.Count + " assignments";
            return View(assignments);
        }

    }
}
