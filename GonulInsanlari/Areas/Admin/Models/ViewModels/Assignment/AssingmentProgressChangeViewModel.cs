namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public class AssingmentProgressChangeViewModel
    {
        public string SubTaskId { get; set; } = null!;

        public int TaskId { get; set; }
        public string Progress { get; set; }

    }
}
