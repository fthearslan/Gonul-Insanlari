using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ViewModelLayer.ViewModels.Comment;

namespace ViewModelLayer.ViewModels.Article
{
    public record ArticleDetailsUIViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public int SeenCount { get; set; }
        public DateTime Created { get; set; }



    }
}
