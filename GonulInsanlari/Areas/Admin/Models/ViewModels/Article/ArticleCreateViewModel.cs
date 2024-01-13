using EntityLayer;
using System.Security.Policy;
using GonulInsanlari.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public class ArticleCreateViewModel
    {

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Title cannot contain more than 50 charachter.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(15000, MinimumLength = 500, ErrorMessage = "Article content cannot contain less than 500 character.")]
        public string Content { get; set; } = null!;

        public DateTime Created { get; set; }=DateTime.Now;

        [Required(ErrorMessage = "Please, select an image for this article.")]

        public IFormFile ImagePath { get; set; } = null!;
        public int? CategoryID { get; set; }

        public bool Status { get; set; } = true;    
        public Category? Category { get; set; }

        public IFormFile VideoPath { get; set; } = null!;

        public bool IsDraft { get; set; } = false;

      
     
    }
}
