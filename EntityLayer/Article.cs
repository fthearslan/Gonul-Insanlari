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
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Edited { get; set; }
        public string? EditedBy { get; set; }

        public bool? Status { get; set; }
        public bool? IsDraft { get; set; }

        public string ImagePath { get; set; } = null!;

        public int CategoryID { get; set; } 
        
        public Category Category { get; set; } = null!;

        public List<Comment> Comments { get; set; } = null!;

        public Video? Video { get; set; }
        public AppUser AppUser { get; set; } = null!;


    }
}
