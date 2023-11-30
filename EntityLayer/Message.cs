using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Message
    {
        [Key]
        public int MessageID {  get; set; }

        [StringLength(100)]
        public string Subject { get; set; }
        [StringLength(300)]
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public bool IsDraft { get; set; }
        public bool IsSeen { get; set; }
        public AppUser Sender { get; set; }
        public AppUser Receiver { get; set; }
    }
}
