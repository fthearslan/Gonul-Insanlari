using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class UserNotification
    {

        public UserNotification(int userId, int notificationId)
        {

            UserId = userId;
            NotificationId = notificationId;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id = Guid.NewGuid();

        public int UserId { get; set; }

        public int NotificationId { get; set; }


        public AppUser User { get; set; }

        public Notification Notification { get; set; }

        public bool IsSeen { get; set; } = false;


    }
}

