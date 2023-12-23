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
        public string Path { get; set; }
        public DateTime? Created { get; set; }
        public bool Status { get; set; }
        public bool IsUrl { get; set; }
        public List<ArticleVideo> Articles { get; set; }

        public  AppUser? AppUser { get; set; }
    }
}
