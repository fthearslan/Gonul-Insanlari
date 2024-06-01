using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Http;

namespace ViewModelLayer.ViewModels.TaskAttachment
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
