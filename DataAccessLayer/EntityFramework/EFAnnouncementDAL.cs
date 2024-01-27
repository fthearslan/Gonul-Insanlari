using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFAnnouncementDAL : GenericRepository<Announcement>, IAnnouncementDAL
    {
        public List<Announcement> GetForAdmins()
        {
            using (var c= new Context())
            {
            
                return c.Announcements
                    .Where(a=>a.IsForAdmins==true)
                    .Include(a => a.CreatedBy)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
           
            } 
        
        }
    }
}
