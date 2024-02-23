using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record AssignmentCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public DateTime Created => DateTime.Now;
        [Required]
        public DateTime Due { get; set; }

        public DateTime StartDate { get; set; }
        public IFormFile? Attachment { get; set; }
        [Required]
        public List<int> Users { get; set; } = null!;

    }
}
