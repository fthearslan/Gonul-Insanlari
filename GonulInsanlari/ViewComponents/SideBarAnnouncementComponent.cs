using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Announcement;

namespace GonulInsanlari.ViewComponents
{
    public class SideBarAnnouncementComponent : ViewComponent
    {
        IAnnouncementService _manager;

        public SideBarAnnouncementComponent(IAnnouncementService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {
            List<AnnouncementSideBarViewModel> models = _manager.GetWhere(x => x.Status == true && x.IsForAdmins == false)
                .OrderByDescending(x => x.IsAttached == true)
                .Select(x => new AnnouncementSideBarViewModel()
                {
                    ID = x.Id,
                    Title = x.Title,
                    Created = x.Created,

                }).ToList();

            return View(models);


        }


    }
}
