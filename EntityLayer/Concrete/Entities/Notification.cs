using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class Notification : BaseEntity
    {

        [StringLength(75)]
        public string Title { get; set; }
        [StringLength(30)]
        public string? Symbol { get; set; }
        public string Type { get; set; }
        [StringLength(150)]
        public string Content { get; set; }
        public int? Value { get; set; }
        public bool IsSeen { get; set; } = false;
        public NotificationResultType ResultType { get; set; } = NotificationResultType.success;
        public List<UserNotification> Users
        { get; set; }

    }

    public enum NotificationResultType
    {
        success,
        warning



    }
}
