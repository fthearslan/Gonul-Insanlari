using GonulInsanlari.Models;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Category
{
    public class CategoryCreateViewModel
    {
        [Required]
        [StringLength(30,ErrorMessage ="Name cannot contain more than 30 chrachters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:140, ErrorMessage = "Description cannot contain less than 15 and more than 140 chrachters.", MinimumLength = 15)]
        public string Description { get; set; } = null!;

        [Required]
        public IFormFile Image { get; set; } = null!;

        public string? ImagePath { get; set; }

        public bool Status => true;

        public async Task SetImagePath()
        {
           this.ImagePath = await ImageUpload.UploadAsync(this.Image);
        }

    }

}
