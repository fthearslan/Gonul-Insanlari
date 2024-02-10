using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetAssignments:ViewComponent
    {
        UserManager<AppUser> _userManager;

        private readonly IAssignmentService _manager;

        public GetAssignments(UserManager<AppUser> userManager, IAssignmentService manager)
        {
            _userManager = userManager;
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var user =  _userManager.GetUserAsync(HttpContext.User).Result;
            var assignments =  _manager.GetAssignmentsByReceiver(user.Id);
            return View(assignments);

        }
    }
}
