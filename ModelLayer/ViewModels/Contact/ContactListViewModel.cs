using EntityLayer.Concrete.Entities;
using System.ComponentModel.DataAnnotations;

namespace ViewModelLayer.ViewModels.Contact
{
    public record ContactListViewModel
    {
        public int Id { get; set; }

        public string NameSurname { get; set; } = null!;

        public string From { get; set; } = null!;

        public string To { get; set; } = null!;
        public string Content { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public ContactStatus ContactStatus { get; set; }

        public bool IsSeen { get; set; }
        public string? CreatedDate { get; set; }
    
        public DateTime Created { get; set; }
    }
}
