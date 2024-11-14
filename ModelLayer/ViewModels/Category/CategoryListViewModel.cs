using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Policy;

namespace ViewModelLayer.ViewModels.Category
{
    public record struct CategoryListViewModel
    {
        public int CategoryID { get; set; }

        public string? Name { get; set; }

        public bool Status { get; set; }


        public int ArticleCount { get; set; }

    }
}
