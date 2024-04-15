using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

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

            if (announcements.Any())
            {
                List<AnnouncementBarViewModel> model = new();
                foreach (var announcement in announcements)
                {
                    model.Add(new()
                    {
                        Id = announcement.Id,
                        Title = announcement.Title,
                        Details = announcement.Details,
                        Created = announcement.Created,

                    });

                }

               
                ViewBag.Count = model.Count;

                return View(model);
            }
            return View();

        }
        public record AnnouncementBarViewModel
        {
            public int Id { get; set; }
            public string? Title { get; set; }

            string? _details;
            public string Details
            {

                get
                {
                    return _details;
                }
                set
                {
                    _details = value;
                }

            }

            public DateTime Created { get; set; }
        }
    }
}
