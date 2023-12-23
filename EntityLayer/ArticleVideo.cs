using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ArticleVideo
    {
        public int ArticleID { get; set; }
        public Article Article { get; set; }
        public int VideoID { get; set; }
        public Video Video { get; set; }

    }
}
