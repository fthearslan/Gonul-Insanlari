using EntityLayer;
using JetBrains.Annotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public class ArticleListViewModel
    {

        public int ArticelID { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public string ImagePath { get; set; }
        public string AppUser { get; set; }
        public DateTime Created { get; set; }

        public int CommentCount { get; set; }


    }
}
