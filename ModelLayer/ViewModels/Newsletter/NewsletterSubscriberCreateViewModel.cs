using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Newsletter
{
    public record NewsletterSubscriberCreateViewModel
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MailAddress { get; set; }


    }
}
