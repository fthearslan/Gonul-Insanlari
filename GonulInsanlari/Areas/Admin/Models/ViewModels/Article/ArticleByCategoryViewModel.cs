namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Article
{
    public record struct ArticleByCategoryViewModel
    {
        public int ArticleID { get; set; }
        public string? Title { get; set; }
        public string? AppUserName { get; set; }

    }
}
