namespace ViewModelLayer.ViewModels.Article
{
    public record struct ArticleAllViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string AppUser { get; set; }
        public bool Status { get; set; }

        public int SeenCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime Created { get; set; }

    }
}
