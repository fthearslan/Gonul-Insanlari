using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
   public class UserAssignment
    {
        public int UserId { get; set; }
        public int AssignmentId { get; set; }

        public AppUser User { get; set; } = null!;
        public Assignment Assignment { get; set; } = null!;

    }
}
