using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public class ArticleEditViewModel
    {


        public int ArticleID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title cannot contain less than 3 charachters.")]
        public string Title { get; set; } = null!;

        [Required]
        public int? CategoryID { get; set; }

        public DateTime Created { get; set; }

        [Required]
        [StringLength(15000, MinimumLength = 500, ErrorMessage = "Too short for article.")]
        public string Content { get; set; } = null!;
        [Required(ErrorMessage = "Please, select an image for this article.")]
        public string? ImagePath { get; set; }

        public IFormFile? Image { get; set; }

        public string? VideoPath { get; set; }
        public bool IsDraft { get; set; } = false;

        public int AppUserID { get; set; }


        public bool Status { get; set; } = true;
        public void GetVideoUrl(string? Url)
        {

            VideoPath = GetUrl.GetVideoUrl(Url);

        }

        public async Task GetImage(IFormFile? image) 
        {
           
         ImagePath = await ImageUpload.UploadAsync(image);

        }
    }
}
