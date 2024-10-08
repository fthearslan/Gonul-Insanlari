using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Comment
{
    public record CommentReplyViewModel
    {

        public string NameSurname { get; set; } = "Admin";
        public string Email { get; set; } = "gonulinsanlari@gmail.com";
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int CommentId { get; set; }
        

    }
}
