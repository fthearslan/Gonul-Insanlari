using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Newsletter
{
    public record NewsletterSubscriberViewModel
    {

        public string Name { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public Guid SecurityStamp { get; set; }
    }
}
