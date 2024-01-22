using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Policy;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Category
{
    public class CategoryListViewModel
    {
        public int CategoryID { get; set; }

        public string? Name { get; set; }

        public bool Status { get; set; }

        public int ArticleCount { get; set; }

    }
}
