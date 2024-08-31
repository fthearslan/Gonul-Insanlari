using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetNotifications : ViewComponent
    {
        private readonly INotificationService _manager;
        private readonly IMapper _mapper;

        public GetNotifications(INotificationService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var notifications = _manager.
                GetWhere(x => x.IsSeen == false && x.Status == true)
                .OrderByDescending(x=>x.Created);

            ViewData["Count"] = notifications.Count();

            List<NotificationBarViewModel> model = _mapper.Map<List<NotificationBarViewModel>>(notifications);

            return View(model.Take(3).ToList());

        }
    }
}
