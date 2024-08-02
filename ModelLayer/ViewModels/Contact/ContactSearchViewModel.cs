using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Contact
{
    public record ContactSearchViewModel
    {

        public string Search { get; set; }

        public ContactStatus ContactStatus { get; set; }

        public bool GetAll { get; set; } = false;


    }
}
