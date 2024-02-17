using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;
using System.Web;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement
{
    public record struct DashboardAnnouncementViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }

        public DateTime Created { get; set; }
        
    }
}
