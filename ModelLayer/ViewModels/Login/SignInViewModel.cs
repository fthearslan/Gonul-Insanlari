using System.ComponentModel.DataAnnotations;

namespace ViewModelLayer.ViewModels.Login
{
    public record SignInViewModel
    {
        [Required(ErrorMessage = "Please, provide a username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please, provide a password.")]
        public string Password { get; set; }
    }
}
