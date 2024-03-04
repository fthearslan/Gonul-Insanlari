using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetMessages : ViewComponent
    {
        private readonly IMessageService _messageManager;
        UserManager<AppUser> userManager;

        public GetMessages(UserManager<AppUser> userManager,IMessageService messageManager)
        {
            this.userManager = userManager;
            _messageManager = messageManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var messages = _messageManager.GetListWithSender(user.Id).Take(3).ToList();
            ViewBag.Count=_messageManager.GetListWithSender(user.Id).Count;
            if (messages.Count != ViewBag.Count)
            {
                ViewBag.Rest = ViewBag.Count - messages.Count;

            }
            return View(messages);
        }
    }
}
