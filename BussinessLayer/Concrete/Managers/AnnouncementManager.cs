using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Managers
{
    public class AnnouncementManager : IAnnouncementService
    {
        IAnnouncementDAL _announcement;

        public AnnouncementManager(IAnnouncementDAL announcement)
        {
            _announcement = announcement;
        }

        public async Task AddAsync(Announcement entity)
        {
            await _announcement.InsertAsync(entity);
        }

        public void Delete(Announcement entity)
        {
            _announcement.Delete(entity);
        }

        public async Task<Announcement> GetByIdAsync(int id)
        {
            return await _announcement.GetAsync(x => x.Id == id);
        }

        public List<Announcement> GetForAdmin()
        {
            return _announcement.GetForAdmin();
        }

        public async Task<List<Announcement>> GetForAdminAsync()
        {
            return await _announcement.GetForAdminAsync();
        }

        public IQueryable<Announcement> GetWhere(Expression<Func<Announcement, bool>> filter)
        {
            return _announcement.GetWhere(filter);
        }

        public async Task<Announcement> GetWithUserAsync(int id)
        {
            return await _announcement.GetWithUserAsync(id);

        }

        public void InsertWithRelated(Announcement entity)
        {
            _announcement.InsertWithRelated(entity);
        }

        public List<Announcement> List()
        {
            return _announcement.List();
        }

        public List<Announcement> ListFilter()
        {
            return _announcement.ListFilter(x => x.Status == true && x.IsForAdmins == true).OrderByDescending(x => x.Created).ToList();
        }

        public void Update(Announcement entity)
        {
            _announcement.Update(entity);
        }
    }
}
