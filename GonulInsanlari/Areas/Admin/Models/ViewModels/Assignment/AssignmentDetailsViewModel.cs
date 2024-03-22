using EntityLayer.Concrete.Entities;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public class AssignmentDetailsViewModel
    {
        public AssignmentDetailsViewModel()
        {
            Users = new();
        }

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Publisher { get; set; }

        public List<(int,string,string)?> Users { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Due { get; set; }

        public string? Progress { get; set; }

    }

    public record SubTaskViewModel
    {

        public Guid Id { get; set; }
        public string? Description { get; set; }

        public string? Progress { get; set; }
    }


}



