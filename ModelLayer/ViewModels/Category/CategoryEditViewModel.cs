using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using ViewModelLayer.Models.Tools;

namespace ViewModelLayer.ViewModels.Category
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }
      
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImagePath { get; set; }

        public bool Status => true;

        public async Task SetImagePath()
        {
            ImagePath = await ImageUpload.UploadAsync(Image);
        }


    }
}
