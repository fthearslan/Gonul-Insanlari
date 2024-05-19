using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Comment
{
    public record CommentListViewModel
    {
        public int Id { get; set; }
        public string? NameSurname { get; set; }
        public string? Content { get; set; }
        public bool? IsApproved { get; set; }
        public bool? Status { get; set; }
        public DateTime? Created { get; set; }

    }
}
