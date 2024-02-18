namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record struct AssignmentBarViewModel
    {
         public int AssignmentId { get; set; }
        public string Title { get; set; }
        public DateTime Created {  get; set; }
        public DateTime Due { get; set; }

        public string Content { get; set; }


    }
}
