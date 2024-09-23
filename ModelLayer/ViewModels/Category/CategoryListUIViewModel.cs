using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Article;

namespace ViewModelLayer.ViewModels.Category
{


    [DebuggerDisplay("Id={Id},Name ={Name,nq}")]
    public record CategoryListUIViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int ArticleCount { get; set; }
        public DateTime Created { get; set; }

    }
}
