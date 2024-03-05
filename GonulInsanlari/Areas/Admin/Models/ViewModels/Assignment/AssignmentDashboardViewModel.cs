namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record struct AssignmentDashboardViewModel
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public DateTime Due {  get; set; }
        public int Progress { get; set; }

    }



}
