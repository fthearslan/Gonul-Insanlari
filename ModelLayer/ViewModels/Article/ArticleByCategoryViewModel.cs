namespace ViewModelLayer.ViewModels.Article
{
    public record ArticleByCategoryViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AppUserName { get; set; }

    }
}
