using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace ViewModelLayer.ViewModels.Admin
{
    public record AdminEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string AboutMe { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string PhoneNumber { get; set; }



    }
}
