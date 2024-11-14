using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;
using System.Security.Claims;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AdminSideBar : ViewComponent
    {
        UserManager<AppUser> _UserManager;

        public AdminSideBar(UserManager<AppUser> userManager)
        {
            _UserManager = userManager;


        }

        public IViewComponentResult Invoke()
        {
            var user = _UserManager.GetUserAsync(HttpContext.User).Result;

            List<string> permissions = _UserManager.GetUserPermissions(HttpContext.User);


            using var c = new Context();


            if (permissions.Contains("Contact.Read"))
            {
                ViewData["ContactCount"] = c.Contacts
                                            .Count(x => x.ContactStatus == ContactStatus.Received && x.IsSeen == false && x.Status == true);
            }

            if (permissions.Contains("Comment.Read"))
            {

                ViewData["CommentCount"] = c.Comments
                                            .Count(x => x.Progress == CommentProgress.Pending && x.Status == true);

            }



            return View(user);
        }
    }
}
