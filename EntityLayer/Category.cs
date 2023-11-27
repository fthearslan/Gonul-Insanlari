using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(maximumLength:75)]
        public string Name { get; set; }

        [StringLength(maximumLength:150)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
        public List<Article> Articles { get; set; }
        public List<Video> Videos { get; set; }
    }
}
