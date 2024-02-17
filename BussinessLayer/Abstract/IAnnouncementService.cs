using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IAnnouncementService:IGenericService<Announcement>
    {
        Task<List<Announcement>> GetForAdminAsync();
        List<Announcement> GetForAdmin();
        Task<Announcement> GetWithUserAsync(int id);
    }
}
