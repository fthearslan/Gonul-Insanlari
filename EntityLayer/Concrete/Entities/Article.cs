using EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{


    [DebuggerDisplay("Id={Id},Title ={Title}")]
    public class Article:BaseEntity
    {

    
        [StringLength(maximumLength: 50)]
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? EditedBy { get; set; }
        public bool? IsDraft { get; set; }
        public string ImagePath { get; set; } = null!;
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;
        public List<Comment> Comments { get; set; } = null!;
        public int? AppUserID { get; set; }
        public string? VideoPath { get; set; }
        public AppUser? AppUser { get; set; }

        public int SeenCount { get; set; }

      
    }
}
