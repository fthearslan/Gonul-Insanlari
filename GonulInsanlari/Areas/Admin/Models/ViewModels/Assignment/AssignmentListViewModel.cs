using MessagePack.Formatters;
using System.ComponentModel;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record AssignmentListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public DateTime Created { get; set; }

        public int SubTasks { get; set; }

        public string Progress { get; set; }
    
        public int UserCount { get; set; }
    
    }

    public record AssignmentByProgressListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public DateTime Created { get; set; }

        public int SubTasks { get; set; }


        public int SubTasksDone { get; set; }


        public string Content { get; set; }

        public List<string> UserImagePaths { get; set; }
    }

    public record AssignmentBarViewModel 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public DateTime Created { get; set; }

        public int SubTasks { get; set; }

        public int SubTasksDone { get; set; }

        public string Content { get; set; }

        public DateTime Due { get;set; }
    }
}
