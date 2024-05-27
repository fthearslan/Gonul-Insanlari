namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Admin
{
    public record AdminEditViewModel
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string PasswordConfirmed { get; set; }

        public string AboutMe { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }






    }
}
