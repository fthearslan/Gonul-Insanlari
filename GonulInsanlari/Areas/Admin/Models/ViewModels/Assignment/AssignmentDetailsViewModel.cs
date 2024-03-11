using EntityLayer.Concrete.Entities;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record AssignmentDetailsViewModel
    {
        public AssignmentDetailsViewModel()
        {
            SubTasks = new();
            Users = new();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Publisher { get; set; }

        public Dictionary<int, string> Users { get; set; }

        public List<SubTaskViewModel> SubTasks { get; set; }

        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Due { get; set; }

    }

    public record SubTaskViewModel
    {

        public Guid Id { get; set; }
        public string Description { get; set; }

        public string Progress { get; set; }
    }


}



