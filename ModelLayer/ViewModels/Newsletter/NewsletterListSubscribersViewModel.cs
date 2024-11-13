using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Newsletter
{
   public record NewsletterListSubscribersViewModel
    {

        public string Id { get; set; }

        public string NameSurname { get; set; }

        public string MailAddress { get; set; }

        public DateTime Created { get; set; }
        public DateTime? SubscriptionStartDate{ get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public SubscriberStatus SubscriberStatus { get; set; }

    }
}
