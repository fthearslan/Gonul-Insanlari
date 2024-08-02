using EntityLayer.Concrete.Entities;

namespace ViewModelLayer.ViewModels.Contact
{
    public record ContactDetailsViewModel
    {

        public ContactDetailsViewModel()
        {
            Attachments = new();
        }

        public int Id { get; set; } 

        public string NameSurname { get; set; }

        public string From { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }

        public ContactStatus ContactStatus { get; set; }
       
        public bool Status { get; set; }
        public string? To { get; set; }

        public List<string> Tos { get; set; } = null!;
        public ContactDetailsViewModel? RepliedTo { get;set; }
        public List<string> Attachments { get; set; } = null!;
        public DateTime Created { get; set; }

    }
}
