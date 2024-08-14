using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Email
{
    public record ConfirmEmailViewModel
    {
        public ConfirmEmailViewModel()
        {
            
        }
        public ConfirmEmailViewModel(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
