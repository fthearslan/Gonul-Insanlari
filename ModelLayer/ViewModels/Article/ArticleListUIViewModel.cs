using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Article
{
    public record ArticleListUIViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int SeenCount { get; set; }
        public DateTime Created { get; set; }





    }
}
