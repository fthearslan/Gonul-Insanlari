using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class GetNotifications : ViewComponent
    {
        private readonly INotificationService _manager;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public GetNotifications(INotificationService manager, IMapper mapper, UserManager<AppUser> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(List<string> permissions)
        {
            string userId = _userManager.GetUserId(UserClaimsPrincipal);

            var notifications = _manager
                .GetPermittedNotifications(permissions, userId)
                .Result.OrderByDescending(x=>x.Notification.Created);

            ViewData["Count"] = notifications
                .Where(x => x.IsSeen == false)
                .Count();

            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(notifications);

            return View(model.Take(3).ToList());

        }
    }
}
