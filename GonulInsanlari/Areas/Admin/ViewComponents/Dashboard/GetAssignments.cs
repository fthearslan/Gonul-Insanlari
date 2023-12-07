using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetAssignments:ViewComponent
    {
        UserManager<AppUser> _userManager;
        
        AssignmentManager AssignmentManager = new AssignmentManager(new EFAssignmentDAL());

        public GetAssignments(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            var assignments = AssignmentManager.GetAssignmentsByReceiver(user.Id);
            return View(assignments);
        }
    }
}
