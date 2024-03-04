namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public record struct ArticleAllViewModel
    {

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public  EntityLayer.Concrete.Entities.Category Category { get; set; }
        public string AppUser { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }

    }
}
