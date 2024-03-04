using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface IAnnouncementDAL : IRepository<Announcement>
    {
        Task<List<Announcement>> GetForAdminAsync();
        List<Announcement> GetForAdmin();

        Task<Announcement> GetWithUserAsync(int id);

    }
}
