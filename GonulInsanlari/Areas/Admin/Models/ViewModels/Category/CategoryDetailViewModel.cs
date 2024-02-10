namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Category
{
    public record struct CategoryDetailViewModel
    {
        public int CategoryID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        public DateTime Created { get; set; }

    }
}
