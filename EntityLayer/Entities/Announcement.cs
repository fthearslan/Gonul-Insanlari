using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Announcement
    {
        [Key]
        public int ID { get; set; }
        [StringLength(75)]
        public string Title { get; set; }
        [StringLength(300)]
        public string Details { get; set; }
        [StringLength(30)]
        public string Subject { get; set; }
        public DateTime? Created { get; set; }
        public bool Status { get; set; }
        public bool IsForAdmins { get; set; }
        public AppUser CreatedBy { get; set; } = null!;

    }
}
