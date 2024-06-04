using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace ViewModelLayer.ViewModels.Admin
{
    public record AdminEditViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }

    }
}
