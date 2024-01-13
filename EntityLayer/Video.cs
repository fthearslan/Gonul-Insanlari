using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Video
    {

        [Key]
        public int VideoID { get; set; }
        public string Path { get; set; }
        public DateTime? Created { get; set; }
        public bool Status { get; set; }
        public bool IsUrl { get; set; } // will be deleted.
        public int ArticleID { get; set; }

        public Article? Article { get; set; }
    }
}
