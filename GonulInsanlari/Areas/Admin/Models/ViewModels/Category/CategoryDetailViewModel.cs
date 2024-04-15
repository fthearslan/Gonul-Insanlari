using System.Diagnostics;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Category
{
    [DebuggerDisplay("Id={Id},Name ={Name,nq}")]
    public record  CategoryDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        public DateTime Created { get; set; }

    }
}
