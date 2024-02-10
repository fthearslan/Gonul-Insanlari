using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AnnounceBar:ViewComponent
    {
        private readonly IAnnouncementService _manager;

        public AnnounceBar(IAnnouncementService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var announcements=_manager.ListFilter().Take(6).ToList();
           var count = _manager.ListFilter().Count;
            ViewBag.Count = count;
            return View(announcements);
        }
    }
}
