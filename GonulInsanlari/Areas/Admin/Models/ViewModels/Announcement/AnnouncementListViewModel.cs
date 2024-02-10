namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement
{
    public record struct AnnouncementListViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public string Subject { get; set; }

        public DateTime Created { get; set; }

        public string User{ get; set; }

        public bool IsForAdmins { get; set; }

    }
}
