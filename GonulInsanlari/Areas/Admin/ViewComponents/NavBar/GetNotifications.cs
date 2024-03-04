using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetNotifications : ViewComponent
    {
        private readonly INotificationService _manager;

        public GetNotifications(INotificationService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var notifications = _manager.ListFilter().Take(3).ToList();
            ViewBag.Count = _manager.ListFilter().Count;   
            if(notifications.Count != ViewBag.Count)
            {
                ViewBag.Rest = ViewBag.Count - notifications.Count;

            }
            return View(notifications);
        }
    }
}
