using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IAnnouncementDAL:IRepository<Announcement>
    {
        Task<List<Announcement>> GetForAdminAsync();
        List<Announcement> GetForAdmin();

        Task<Announcement> GetWithUserAsync(int id);

    }
}
