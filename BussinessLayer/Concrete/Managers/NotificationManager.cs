using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TableDependency.SqlClient;
using ViewModelLayer.ViewModels.Notification;
using X.PagedList;

namespace BussinessLayer.Concrete.Managers
{
    public class NotificationManager : INotificationService
    {
        INotificationDAL _notify;
        public NotificationManager(INotificationDAL notify)
        {
            _notify = notify;
        }

        public async Task AddAsync(Notification entity)
        {
            await _notify.InsertAsync(entity);
        }


        public void Delete(Notification entity)
        {
            _notify.Delete(entity);
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _notify.GetAsync(x => x.Id == id);
        }

        public async Task<List<UserNotification>> GetPermittedNotifications(List<string> permissions, string userId)
        {

            if (permissions is null)
                return null;

            List<UserNotification> notifications = await _notify.GetNotifications(userId);

            List<UserNotification> result = new();

            notifications?.ForEach(notification =>
            {

                if (permissions.Contains(notification.Notification.Type + ".Read"))
                    result.Add(notification);

            });

            return result;

        }

        public async Task<UserNotification> GetUserNotificationById(string userId, int notificationId)
        {
            return await _notify.GetUserNotificationById(userId,notificationId);
        }

        public IQueryable<Notification> GetWhere(Expression<Func<Notification, bool>> filter)
        {
            return _notify.GetWhere(filter);
        }

        public void InsertWithRelated(Notification entity)
        {
            _notify.InsertWithRelated(entity);
        }

        public List<Notification> List()
        {
            return _notify.List();
        }

        public List<Notification> ListFilter()
        {
            return _notify.ListFilter(x => x.Status == true).OrderByDescending(x => x.Created).ToList();
        }

        public async Task<List<UserNotification>> SearchNotifications(NotificationSearchViewModel model)
        {

            if (model.UserPermissions is null)
                return null;

            List<UserNotification> notifications = await _notify.SearchNotifications(model.UserId, model.SearchInput);

            List<UserNotification> result = new();

            notifications?.ForEach(notification =>
            {

                if (model.UserPermissions.Contains(notification.Notification.Type + ".Read"))
                    result.Add(notification);

            });

            return result;

        }

        public void Update(Notification entity)
        {
            _notify.Update(entity);
        }

        public async Task UpdateUserNotification(UserNotification userNotification)
        {
            await _notify.UpdateUserNotification(userNotification);

        }
    }
}
