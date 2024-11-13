using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Article;

namespace ViewModelLayer.ViewModels.Footer
{
    public record FooterViewModel
    {

        public FooterViewModel()
        {
            Articles = new();
            Categories= new();
        }
        public List<ArticleSideBarViewModel> Articles {  get; set; }

        public List<FooterCategoryViewModel > Categories { get; set; }



    }

    public record FooterCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       

    }


}
