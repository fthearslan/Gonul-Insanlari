using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Login
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Please, provide a username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please, provide a password.")]
        public string Password { get; set; }
    }
}
