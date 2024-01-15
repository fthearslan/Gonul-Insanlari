using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Video
    {


        public int VideoId { get; set; }
        public string Path { get; set; } = null!;
        public bool Status { get; set; }
        public int ArticleID { get; set; }
        public Article Article { get; set; } = null!;
        
        

    }
}
