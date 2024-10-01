using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Comment
{
    public record CommentSubmitUIViewModel
    {
        public string? NameSurname { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int ArticleID { get; set; }

    }
}
