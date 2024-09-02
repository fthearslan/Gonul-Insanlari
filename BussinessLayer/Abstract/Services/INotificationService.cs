using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface INotificationService : IGenericService<Notification>
    {
        Task<List<Notification>> SearchNotifications(string searchInput);
        Task<List<Notification>> GetPermittedNotifications(List<string> permissions);

    }
}
