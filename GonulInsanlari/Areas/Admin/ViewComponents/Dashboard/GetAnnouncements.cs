using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetAnnouncements:ViewComponent
    {
        private readonly IAnnouncementService _manager;

        public GetAnnouncements(IAnnouncementService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var announcements = _manager.ListFilter().Take(3).ToList();
            return View(announcements);
        }
    }
}
