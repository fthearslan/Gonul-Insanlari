using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore;
using EntityLayer.Concrete.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFAnnouncementDAL : GenericRepository<Announcement>, IAnnouncementDAL
    {
     
        public List<Announcement> GetForAdmin()
        {
            using var db = new Context();
                return db.Announcements
                    .Where(a => a.IsForAdmins == true)
                    .Include(a => a.User)
                    .OrderByDescending(c => c.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
            
        }

        public async  Task<List<Announcement>> GetForAdminAsync()
        {
            using var db = new Context();
                return await db.Announcements
                    .Where(a => a.IsForAdmins == true)
                    .Include(a => a.User)
                    .OrderByDescending(c => c.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            

        }

        public async Task<Announcement> GetWithUserAsync(int id)
        {
            using var db = new Context();
            return await db.Announcements
                  .Where(a => a.Id == id)
                  .Include(a => a.User)
                  .AsNoTrackingWithIdentityResolution()
                  .FirstOrDefaultAsync();

        }
    }
}
