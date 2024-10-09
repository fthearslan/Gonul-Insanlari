using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Contact
{
    public record SubmitContactViewModel
    {
        [DisplayName("Name and Surname")]
        public string NameSurname { get; set; }

        [DisplayName("Email")]
        public string From { get; set; }

        [DisplayName("Message")]

        public string Content { get; set; }
        
        public string Subject { get; set; }

       




    }
}
