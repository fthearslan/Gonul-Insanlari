using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)] 
        public string NameSurname { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(300)]
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool IsSeen { get; set; }
        public bool Status { get; set; }


    }
}
