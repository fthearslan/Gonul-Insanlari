using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFAnnouncementDAL : GenericRepository<Announcement>, IAnnouncementDAL
    {
        public List<Announcement> GetForAdmin()
        {
            using (var c = new Context())
            {

                return c.Announcements
                    .Where(a => a.IsForAdmins == true)
                    .Include(a => a.User)
                    .OrderByDescending(c => c.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
            }
        }

        public async  Task<List<Announcement>> GetForAdminAsync()
        {
            using (var c = new Context())
            {

                return await  c.Announcements
                    .Where(a => a.IsForAdmins == true)
                    .Include(a => a.User)
                    .OrderByDescending(c => c.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            }

        }

        public async Task<Announcement> GetWithUserAsync(int id)
        {
            using var c = new Context();
            {
                return await c.Announcements
                      .Where(a => a.ID == id)
                      .Include(a => a.User)
                      .AsNoTrackingWithIdentityResolution()
                      .FirstOrDefaultAsync();
            }

        }
    }
}
