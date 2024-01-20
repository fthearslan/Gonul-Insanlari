using JetBrains.Annotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public class ArticleDetailsViewModel
    {

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AppUser { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Edited { get; set; } 
        public string? EditedBy { get; set; }
        public bool IsDraft { get; set; }

        public string? VideoPath { get; set; }
    }
}
