using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Contact
{
    public record ContactListViewModel
    {
        public int Id { get; set; }

        public string NameSurname { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;
        public string Content { get; set; } = null!;

        public string Subject { get; set; } = null!;
        public bool IsSeen { get; set; }
        public string? CreatedDate { get; set; }
        public bool IsSent { get; set; }
        public bool IsDraft { get; set; }
        public DateTime Created { get; set; }
    }
}
