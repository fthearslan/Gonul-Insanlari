using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFNotificationDAL : GenericRepository<Notification>, INotificationDAL
    {
        public async Task<List<UserNotification>> GetNotifications(string userId)
        {

            using var c = new Context();

            return await c.UserNotifications
                .Where(x => x.UserId == Convert.ToInt32(userId))
                .OrderByDescending(x => x.Notification.Created)
                  .OrderByDescending(x => x.IsSeen == false)
                   .ToListAsync();


        }

        public async Task<UserNotification> GetUserNotificationById(string userId,int notificationId)
        {
            using var c = new Context();

            return await c.UserNotifications
              .SingleOrDefaultAsync(x => x.UserId==Convert.ToInt32(userId) && x.Notification.Id == notificationId);


        }

        public async Task<List<Notification>> SearchNotifications(string searchInput)
        {

            using var c = new Context();

            return await c.Notifications
                  .Where(x => x.Title.Contains(searchInput) | x.Content.Contains(searchInput) | x.Type.Contains(searchInput))
                  .OrderByDescending(x => x.Created)
                .OrderByDescending(x => x.IsSeen == false)
                  .ToListAsync();


        }

        public async Task UpdateUserNotification(UserNotification userNotification)
        {

            using var c = new Context();

            c.UserNotifications.Update(userNotification);

            await c.SaveChangesAsync();

        }
    }
}
