using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class NotificationController : Controller
    {
        private readonly INotificationService _manager;

        public NotificationController(INotificationService manager)
        {
            _manager = manager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            var notification = await _manager.GetByIdAsync(id);
            notification.Status = false;
            _manager.Update(notification);

            switch (notification.Type)
            {

                case "Article":
                    return RedirectToAction("GetDetailsByNotification", "Article", notification);
                case "Comment":
                    return RedirectToAction("GetDetails", "Comment");
                case "Video":
                    return RedirectToAction("GetDetails", "Video");
                case "Contact":
                    return RedirectToAction("GetDetails", "Contact");
            }

            return View();
        }



    }
}
