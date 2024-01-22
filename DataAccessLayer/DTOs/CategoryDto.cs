using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }

        public string? Name { get; set; }

        public bool Status { get; set; }

        public int ArticleCount { get; set; }


    }
}
