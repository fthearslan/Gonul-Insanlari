using EntityLayer.Concrete.Entities;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record AddAttachmentViewModel
    {
        public int TaskId { get; set; } 
        public IFormFileCollection Attachments { get; set; } = null!;

       
    }
}
