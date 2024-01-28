using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Content { get; set; }
        public bool Status { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime Created { get; set; }
        public DateTime Due { get; set; }

        public AppUser Sender { get; set; }
        public AppUser? Receiver { get; set; }

    }
}
