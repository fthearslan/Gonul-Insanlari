using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            return await _notify.GetAsync(x => x.ID == id);
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

        public void Update(Notification entity)
        {
            _notify.Update(entity);
        }
    }
}
