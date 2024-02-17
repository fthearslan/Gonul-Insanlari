namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement
{
    public record struct AnnouncementDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public string Subject { get; set; }
        public DateTime Created { get; set; }

        public string AppUser { get; set; }

        public string? EditedBy { get; set; }

    }
}
