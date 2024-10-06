using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.NavBar;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AnnounceBar : ViewComponent
    {
        private readonly IAnnouncementService _manager;

        public AnnounceBar(IAnnouncementService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var announcements = _manager.ListFilter().Take(6).ToList();


            List<AnnouncementBarViewModel> model = new();

            announcements.ForEach((announcement) =>
            {
                model.Add(new()
                {
                    Id = announcement.Id,
                    Title = announcement.Title,
                    Details = announcement.Details,
                    Created = announcement.Created,

                });

            });

            ViewBag.Count = model.Count;

            return View(model);

        }

    }
}
