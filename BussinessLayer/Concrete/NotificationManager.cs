using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        INotificationDAL _notify;

        public NotificationManager(INotificationDAL notify)
        {
            _notify = notify;
        }

        public void Add(Notification entity)
        {
            _notify.Insert(entity);
        }

        public void Delete(Notification entity)
        {
            _notify.Delete(entity);
        }

        public Notification GetById(int id)
        {
            return _notify.Get(x => x.ID == id);
        }

        public List<Notification> List()
        {
            return _notify.List();
        }

        public List<Notification> ListFilter()
        {
            return _notify.ListFilter(x => x.Status == true).OrderByDescending(x=>x.Created).ToList();
        }

        public void Update(Notification entity)
        {
            _notify.Update(entity);
        }
    }
}
