using EntityLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement
{
    public record struct AnnouncementCreateViewModel
    {
        [Required]
        [StringLength(maximumLength: 40, ErrorMessage = "Title cannot contain less than 5 and more than 40 chrachters.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Subject cannot contain less than 5 and more than 30 chrachters.", MinimumLength = 5)]
        public string Subject { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Detail cannot contain less than 100 chrachters.")]
        public string Details { get; set; }
        public bool IsForAdmins { get; set; }

        public DateTime Created => DateTime.Now;
         
    }
}
