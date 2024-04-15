using GonulInsanlari.Models;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Category
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Description cannot contain less than 3 and more than 30 chrachters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:10000, ErrorMessage = "Too long.")]
        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImagePath { get; set; }

        public bool Status => true;

        public async Task SetImagePath()
        {
            this.ImagePath = await ImageUpload.UploadAsync(this.Image);
        }


    }
}
