namespace ViewModelLayer.ViewModels.Assignment
{
    public record struct AssignmentDashboardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Due { get; set; }
        public int Progress { get; set; }

    }



}
