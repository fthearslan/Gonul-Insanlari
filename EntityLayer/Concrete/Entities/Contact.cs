using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class Contact : BaseEntity
    {
        [StringLength(50)]
        public string NameSurname { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(300)]
        public string Content { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        public bool IsSeen { get; set; }
        public bool IsDraft { get; set; }

        public bool IsSent { get; set; }
        public string? SenderId { get; set; }

        public string? To { get; set; }

    }
}
