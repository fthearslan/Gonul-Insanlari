using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class TaskAttachment
    {
        public TaskAttachment()
        {

        }
        public TaskAttachment(Assignment task)
        {
            Assignment = task;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Path { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Assignment Assignment { get; set; } = null!;


    }
}
