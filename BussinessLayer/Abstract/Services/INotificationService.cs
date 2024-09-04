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
        Task<List<UserNotification>> GetPermittedNotifications(List<string> permissions, string userId);
        Task<UserNotification> GetUserNotificationById(string userId, int notificationId);
        Task UpdateUserNotification(UserNotification userNotification);

    }
}
