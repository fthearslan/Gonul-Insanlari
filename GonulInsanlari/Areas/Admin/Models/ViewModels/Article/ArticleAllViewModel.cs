namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public class ArticleAllViewModel
    {

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public  EntityLayer.Entities.Category Category { get; set; }
        public string AppUser { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }

    }
}
