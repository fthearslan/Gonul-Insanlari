using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Abstract
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime Created
        { get; set; }= DateTime.Now;
        public DateTime? Modified { get; set; }

        public bool? Status { get; set; }


    }
}
