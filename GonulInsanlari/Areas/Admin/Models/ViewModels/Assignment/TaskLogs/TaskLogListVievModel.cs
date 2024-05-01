using JetBrains.Annotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment.TaskLogs
{
    public class TaskLogListVievModel
    {

        public Guid Id { get; set; }

        public string? Description { get; set; }
        public string? Createdby { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? AttachmentTitle { get; set; }

        public string? CreatedByImage { get; set; }

    }
}
