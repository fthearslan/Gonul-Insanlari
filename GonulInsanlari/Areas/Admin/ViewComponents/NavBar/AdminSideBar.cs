using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AdminSideBar : ViewComponent
    {
        UserManager<AppUser> _UserManager;

        public AdminSideBar(UserManager<AppUser> userManager)
        {
            _UserManager = userManager;
        
        
        }
       
        public  IViewComponentResult Invoke()
        {
           var user=_UserManager.GetUserAsync(HttpContext.User).Result;

            using var c = new Context();

            ViewData["ContactCount"] =  c.Contacts
                .Where(x => x.ContactStatus == ContactStatus.Received && x.IsSeen == false && x.Status == true)
                .Count();

            return View(user);
        }
    }
}
