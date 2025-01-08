using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Notification;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("notifications")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _manager;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public NotificationController(INotificationService manager, IMapper mapper, UserManager<AppUser> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Route("all")]
        public async Task<IActionResult> List(int pageNumber = 1)
        {

            string userId = _userManager.GetUserId(User);

            List<UserNotification> notifications = await _manager.GetPermittedNotifications(_userManager.GetUserPermissions(User), userId);

            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(notifications);

         
            return View(await model.ToPagedListAsync(pageNumber, 20));

        }




        #region Methods 

        [Route("markAsSeen")]

        public async Task MarkAsSeen(int notificationId)
        {
            string userId = _userManager.GetUserId(User);

            var notification = await _manager.GetUserNotificationById(userId, notificationId);

            notification.IsSeen = true;

            await _manager.UpdateUserNotification(notification);


        }

        [Route("search")]
        public async Task<IActionResult> SearchNotifications(string searchInput)
        {

            List<UserNotification> notifications = await _manager.SearchNotifications(new NotificationSearchViewModel(searchInput)
            {
                UserId = _userManager.GetUserId(User),
                UserPermissions = _userManager.GetUserPermissions(User)
            });


            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(notifications);
       
            return Json(model);

        }

        [HttpPost]
        [Route("markAsSeen/all")]
        public async Task<IActionResult> MarkAsSeen()
        {

            string userId = _userManager.GetUserId(User);

            List<UserNotification> userNotifications = await _manager.GetPermittedNotifications(_userManager.GetUserPermissions(User), userId);

            await _manager.MarkAsSeenUserNotificationsAsync(userNotifications);

            return Json(200);

        }

        #endregion



    }
}
