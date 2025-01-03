﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Article;
using X.PagedList;

namespace ViewModelLayer.ViewModels.Category
{
    [DebuggerDisplay("Id={Id},Name ={Name,nq}")]
    public record CategoryDetailsUIViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = null!;
        public string? ImagePath { get; set; }

        public List<ArticleListUIViewModel> Articles { get; set; }

        public IPagedList<ArticleListUIViewModel> PagedArticles { get; set; } = null!;
        public DateTime Created { get; set; }


    }
}
