using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetAnnouncements:ViewComponent
    {
        AnnouncementManager manager = new AnnouncementManager(new EFAnnouncementDAL());

        public IViewComponentResult Invoke()
        {
            var announcements = manager.ListFilter().Take(3).ToList();
            return View(announcements);
        }
    }
}
