using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Role
{
    public record ListUsersByRoleViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }

        public string? ImagePath { get; set; }

        public bool Status { get; set; }
        public List<string> Roles { get; set; } = null!;


    }
}
