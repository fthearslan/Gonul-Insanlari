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
            
            return View(user);
        }
    }
}
