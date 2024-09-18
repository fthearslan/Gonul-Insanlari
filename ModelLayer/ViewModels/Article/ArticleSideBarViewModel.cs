using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Article
{
    public record ArticleSideBarViewModel
    {

        public string Title { get; set; }   
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public DateTime Created { get; set; }
        


    }
}
