using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Login
{
    public record ResetPasswordViewModel
    {

        [Required]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword),ErrorMessage ="Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Something went wrong, please try again later. If it continues, contact support.")]
        public string userId { get; set; }

        [Required(ErrorMessage = "Something went wrong, please try again later. If it continues, contact support.")]
        public string token { get; set; }

    }
}
