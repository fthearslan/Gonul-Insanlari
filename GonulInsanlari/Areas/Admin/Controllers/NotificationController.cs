using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> List( int pageNumber = 1)
        {

            List<string> permissions = User.Claims.
                 Where(x => x.Type == "Permission")
                 .Select(x => x.Value)
                 .ToList();

            string userId = _userManager.GetUserId(User);

            List<UserNotification> notifications = await _manager.GetPermittedNotifications(permissions, userId);


            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(notifications);

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }




        #region Methods 

        [Route("markAsSeen")]
        
        public async Task MarkAsSeen(int notificationId)
        {
            string userId = _userManager.GetUserId(User);

            var notification = await _manager.GetUserNotificationById(userId,notificationId);

            notification.IsSeen = true;

            await _manager.UpdateUserNotification(notification);


        }

        [Route("search")]
        public async Task<IActionResult> SearchNotifications(string searchInput)
        {

            List<Notification> allNotifications = await _manager.SearchNotifications(searchInput);

            List<Notification> permittedNotifications = new();

            allNotifications?.ForEach(ntf =>
            {

                if (User.HasClaim("Permission", ntf.Type + ".Read"))
                    permittedNotifications.Add(ntf);

            });

            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(permittedNotifications);

            return Json(model);

        }


        #endregion



    }
}
