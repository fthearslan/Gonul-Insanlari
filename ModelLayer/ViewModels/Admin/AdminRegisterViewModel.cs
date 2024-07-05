using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Admin
{
    public record AdminRegisterViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; } = 18;
        public string? PhoneNumber { get; set; } = "Not added yet.";
        public string? ImagePath { get; set; }= "profile.jpg";
      
    }
}
