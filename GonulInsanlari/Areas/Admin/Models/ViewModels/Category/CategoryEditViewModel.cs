using GonulInsanlari.Models;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Category
{
    public class CategoryEditViewModel
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Description cannot contain less than 3 and more than 30 chrachters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 140, ErrorMessage = "Description cannot contain less than 15 and more than 140 chrachters.", MinimumLength = 15)]
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
