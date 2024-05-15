namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Contact
{
    public record ContactDetailsViewModel
    {
        public int Id { get; set; }

        public string NameSurname { get; set; }

        public string EmailAddress { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }


        public DateTime Created { get; set; }
    }
}
