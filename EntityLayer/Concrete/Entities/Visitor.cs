using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class Visitor
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Visited { get; set; } = DateTime.Now;



    }


}
