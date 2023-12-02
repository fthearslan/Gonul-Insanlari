using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        [StringLength(75)]
        public string Title { get; set; }
        [StringLength(30)]
        public string? Symbol { get; set; }
        public string Type { get; set; }
        [StringLength(150)]
        public string Content { get; set; }
        public int? Value { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }

    }
}
