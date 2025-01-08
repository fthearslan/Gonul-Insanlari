using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ViewModelLayer.Models.Tools;

namespace ViewModelLayer.ViewModels.Article
{
    public record ArticleEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public int? CategoryID { get; set; }


        public string Content { get; set; } = null!;
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
