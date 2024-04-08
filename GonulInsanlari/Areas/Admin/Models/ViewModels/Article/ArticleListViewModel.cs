using JetBrains.Annotations;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public record struct ArticleListViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public EntityLayer.Concrete.Entities.Category Category { get; set; }
        public string ImagePath { get; set; }
        public string UserImagePath { get; set; }
        public string AppUser { get; set; }
        public DateTime Created { get; set; }
        public int CommentCount { get; set; }



    }
}
