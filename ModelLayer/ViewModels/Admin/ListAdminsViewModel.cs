using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Admin
{
    public record ListAdminsViewModel
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }
        public string? Username { get; set; }
        public List<string> Roles { get; set; }

        public bool Status { get; set; }


    }
}
