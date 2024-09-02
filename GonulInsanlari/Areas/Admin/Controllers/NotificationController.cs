using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
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
        public NotificationController(INotificationService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [Route("all")]
        public async Task<IActionResult> List(int pageNumber = 1)
        {

            List<string> permissions = User.Claims.
                 Where(x => x.Type == "Permission")
                 .Select(x => x.Value)
                 .ToList();

            List<Notification> notifications = await _manager.GetPermittedNotifications(permissions);


            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(notifications);

            return View(await model.ToPagedListAsync(pageNumber, 20));

        }




        #region Methods 

        [Route("markAsSeen")]
        public async Task MarkAsSeen(int notificationId)
        {
            var notification = await _manager.GetByIdAsync(notificationId);

            notification.IsSeen = true;

            _manager.Update(notification);


        }

        [Route("search")]
        public async Task<IActionResult> SearchNotifications(string searchInput)
        {

            List<Notification> allNotifications = await _manager.SearchNotifications(searchInput);

            List<Notification> permittedNotifications = new();

            allNotifications?.ForEach(ntf =>
            {

                if(User.HasClaim("Permission", ntf.Type + ".Read"))
                    permittedNotifications.Add(ntf);

            });

            List<NotificationListViewModel> model = _mapper.Map<List<NotificationListViewModel>>(permittedNotifications);

            return Json(model);

        }


        #endregion



    }
}
