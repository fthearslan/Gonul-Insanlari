using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Video
    {
        [Key]
        public int VideoID { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(150)]
        public string? Description { get; set; }
        public string  Path { get; set; }
        public string ImagePath { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public List<Comment> Comments { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public DateTime? Edited { get; set; }
        public string? EditedBy { get; set; }
        public int? ArticleID { get; set; }
        public Article? Article { get; set; }
        public AppUser AppUser { get; set; }

    }
}
