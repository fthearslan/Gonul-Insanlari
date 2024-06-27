using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Role
{
   public record AddUserToRoleViewModel
    {

        public int Id { get; set; }

        public string? Username { get; set; }

        public string? ImagePath { get; set; }

    }
}
