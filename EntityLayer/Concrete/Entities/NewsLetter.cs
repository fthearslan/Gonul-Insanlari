using EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class NewsLetter : BaseEntity
    {
        public string? Name { get; set; } = "Unknown";
        public string? Surname { get; set; }
        public string MailAddress { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTime? SubscriptionStartDate { get; set; } 
        public DateTime? SubscriptionEndDate { get; set; }

        public SubscriberStatus SubscriberStatus { get; set; } = SubscriberStatus.Pending;

    }

    public enum SubscriberStatus
    {
        Pending,
        Active

    }

}
