using EntityLayer.Concrete.Entities;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.TaskAttachment
{
    public record AddAttachmentViewModel
    {

        public int TaskId { get; set; }
        public IList<IFormFile> Attachments { get; set; } = null!;



    }

    public enum FileStatus
    {

        Success,
        SizeViolation,
        Error,

    }



}
