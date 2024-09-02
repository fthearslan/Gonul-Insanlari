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

        public async Task<List<Notification>> GetPermittedNotifications(List<string> permissions)
        {

            if (permissions is null)
                return null;

            List<Notification> notifications = await _notify.GetNotifications();
            
            List<Notification> result = new();

            notifications?.ForEach(notification =>
            {

                if (permissions.Contains(notification.Type + ".Read"))
                    result.Add(notification);

            });

            return result;

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

        public async Task<List<Notification>> SearchNotifications(string searchInput)
        {
            if (searchInput is not null)
                await _notify.SearchNotifications(searchInput);

            return null;

        }

        public void Update(Notification entity)
        {
            _notify.Update(entity);
        }



    }
}
