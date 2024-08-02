using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class TaskAttachment:BaseAttachment
    {
        public TaskAttachment()
        {
            Assignment = new Assignment();
        }
        public TaskAttachment(Assignment task)
        {
            Assignment = new Assignment();
            Assignment = task;
        }

           public Assignment Assignment { get; set; } = null!;


    }
}
