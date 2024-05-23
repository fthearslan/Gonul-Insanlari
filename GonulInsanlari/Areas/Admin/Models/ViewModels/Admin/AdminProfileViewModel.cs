using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Admin
{
    public record AdminProfileViewModel
    {
        public int Id {  get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public List<string> Roles { get;set; }

    }
}
