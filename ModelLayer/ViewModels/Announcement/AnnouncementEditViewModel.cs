using System.ComponentModel.DataAnnotations;

namespace ViewModelLayer.ViewModels.Announcement
{
    public record AnnouncementEditViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string Subject { get; set; }

        public string Details { get; set; }
        public bool IsForAdmins { get; set; }
        public int UserId { get; set; }


    }
}
