using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AnnounceBar:ViewComponent
    {
        AnnouncementManager Manager = new AnnouncementManager(new EFAnnouncementDAL());
        public IViewComponentResult Invoke()
        {
            var announcements=Manager.ListFilter().Take(6).ToList();
           var count = Manager.ListFilter().Count;
            ViewBag.Count = count;
            return View(announcements);
        }
    }
}
