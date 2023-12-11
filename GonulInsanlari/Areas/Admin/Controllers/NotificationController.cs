using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        NotificationManager manager = new NotificationManager(new EFNotificationDAL());

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDetails(int id)
        {
            var notification= manager.GetById(id);
         
            switch (notification.Type)
            {
                case "Article":
                    return RedirectToAction("GetDetails", "Article",notification);
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
