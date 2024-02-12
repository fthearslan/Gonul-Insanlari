﻿using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BussinessLayer.Concrete
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

        public async  Task<Notification> GetByIdAsync(int id)
        {
            return await _notify.GetAsync(x => x.ID == id);
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
            return _notify.ListFilter(x => x.Status == true).OrderByDescending(x=>x.Created).ToList();
        }

        public void Update(Notification entity)
        {
            _notify.Update(entity);
        }
    }
}
