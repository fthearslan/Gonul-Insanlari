using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        IAnnouncementDAL _announcement;

        public AnnouncementManager(IAnnouncementDAL announcement)
        {
            _announcement = announcement;
        }

        public void Add(Announcement entity)
        {
            _announcement.Insert(entity);
        }

        public void Delete(Announcement entity)
        {
            _announcement.Delete(entity);
        }

        public Announcement GetById(int id)
        {
            return _announcement.Get(x => x.ID == id);
        }

        public List<Announcement> List()
        {
            return _announcement.List();
        }

        public List<Announcement> ListFilter()
        {
            return _announcement.ListFilter(x => x.Status == true && x.ToPublish == false).OrderByDescending(x=>x.Created).ToList();
        }

        public void Update(Announcement entity)
        {
            _announcement.Update(entity);
        }
    }
}
