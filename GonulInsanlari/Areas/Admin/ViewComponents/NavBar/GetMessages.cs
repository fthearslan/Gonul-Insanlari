using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetMessages : ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EFMessageDAL());
        UserManager<AppUser> userManager;

        public GetMessages(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var messages = messageManager.GetListWithSender(user.Id).Take(3).ToList();
            ViewBag.Count=messageManager.GetListWithSender(user.Id).Count;
            if (messages.Count != ViewBag.Count)
            {
                ViewBag.Rest = ViewBag.Count - messages.Count;

            }
            return View(messages);
        }
    }
}
