using BussinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetNotifications : ViewComponent
    {
        NotificationManager notificationManager = new NotificationManager(new EFNotificationDAL());
        public IViewComponentResult Invoke()
        {
            var notifications = notificationManager.ListFilter().Take(3).ToList();
            ViewBag.Count = notificationManager.ListFilter().Count;
            if(notifications.Count != ViewBag.Count)
            {
                ViewBag.Rest = ViewBag.Count - notifications.Count;

            }
            return View(notifications);
        }
    }
}
