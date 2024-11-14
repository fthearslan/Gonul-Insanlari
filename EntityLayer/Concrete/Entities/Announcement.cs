using EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class Announcement : BaseEntity
    {
     
        [StringLength(75)]
        public string Title { get; set; }
        [StringLength(15000)]
        public string Details { get; set; }
        [StringLength(30)]
        public string Subject { get; set; }
        public bool IsForAdmins { get; set; }

        public bool IsAttached { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; } 
        public string? EditedBy { get; set; }

    }
}
