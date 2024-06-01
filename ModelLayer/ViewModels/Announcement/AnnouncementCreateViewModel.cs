using System.ComponentModel.DataAnnotations;

namespace ViewModelLayer.ViewModels.Announcement
{
    public record AnnouncementCreateViewModel
    {
        public string Title { get; set; }

        public string Subject { get; set; }

        public string Details { get; set; }
        public bool IsForAdmins { get; set; }
        public DateTime Created => DateTime.Now;

    }
}
