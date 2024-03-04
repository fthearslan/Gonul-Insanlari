using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [StringLength(maximumLength: 50)]
        public string NameSurname { get; set; }

        [StringLength(maximumLength: 300)]
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public bool IsApproved { get; set; }
        public int? ArticleID { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? Edited { get; set; }
        public Article Article { get; set; }


    }
}
