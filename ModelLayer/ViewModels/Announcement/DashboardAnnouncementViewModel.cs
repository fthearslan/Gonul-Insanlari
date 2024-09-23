using System.Text.Encodings.Web;
using System.Web;

namespace ViewModelLayer.ViewModels.Announcement
{
    public record  DashboardAnnouncementViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }

        public DateTime Created { get; set; }

    }
}
