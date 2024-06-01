using System.ComponentModel.DataAnnotations;

namespace ViewModelLayer.ViewModels.Comment
{
    public record CommentListViewModel
    {
        public int Id { get; set; }
        public string? NameSurname { get; set; }
        public string? Content { get; set; }
        public string? Progress { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }
        public int ArticleID { get; set; }

    }
}
