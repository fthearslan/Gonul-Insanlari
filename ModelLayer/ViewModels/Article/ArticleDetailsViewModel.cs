using JetBrains.Annotations;

namespace ViewModelLayer.ViewModels.Article
{
    public record struct ArticleDetailsViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AppUser { get; set; }

        public string CategoryName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string? EditedBy { get; set; }
        public bool IsDraft { get; set; }

        public string? VideoPath { get; set; }

    }
}
