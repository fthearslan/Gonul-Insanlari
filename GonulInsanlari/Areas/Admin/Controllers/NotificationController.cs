using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("notifications")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _manager;

        public NotificationController(INotificationService manager)
        {
            _manager = manager;
        }

        [Route("all")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("markAsSeen")]
        public async Task MarkAsSeen(int notificationId)
        {
            var notification = await _manager.GetByIdAsync(notificationId);

            notification.IsSeen = true;

            _manager.Update(notification);


        }



    }
}
