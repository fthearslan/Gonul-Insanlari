using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        [StringLength(maximumLength: 50)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public bool Status { get; set; }

        public string ImagePath { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public List<Comment> Comments { get; set;}

        public List<Video>? Videos { get; set;}
        public AppUser AppUser { get; set; }


    }
}
