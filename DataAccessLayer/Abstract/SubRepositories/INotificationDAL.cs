using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface INotificationDAL : IRepository<Notification>
    {

        Task<List<UserNotification>> SearchNotifications(string userId,string searchInput);

        Task<List<UserNotification>> GetNotifications(string userId);

        Task<UserNotification> GetUserNotificationById(string userId, int notificationId);

        Task UpdateUserNotification(UserNotification userNotification);
    }
}
