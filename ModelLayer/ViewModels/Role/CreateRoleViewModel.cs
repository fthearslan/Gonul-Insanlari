using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Role
{
    public record CreateRoleViewModel
    {

        public string Name { get; set; } = null!;

        public string RoleDescription { get; set; } = null!;

    }
}
