using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Category
{
    public record CategoryWithCountViewModel
    {

        public string Name { get; set; }

        public int ArticleCount { get; set; }


    }
}
