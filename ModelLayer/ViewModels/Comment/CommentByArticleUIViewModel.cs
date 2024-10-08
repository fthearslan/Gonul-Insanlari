using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Comment
{
    public record CommentByArticleUIViewModel
    {

        public int Id { get; set; }
        public string? NameSurname { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }
        public string DisplayedDateTime { get; set; }

        public int ArticleID { get; set; }

        public List<CommentByArticleUIViewModel> Replies { get; set; } = new();


    }
}
