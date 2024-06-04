using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Admin
{
    public record AdminChangePasswordViewModel
    {

        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set;}
        public string? ConfirmPassword { get; set; }
    }
}
