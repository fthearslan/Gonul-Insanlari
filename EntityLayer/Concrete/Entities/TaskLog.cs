using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class TaskLog
    {
        public TaskLog() { }

        public TaskLog(string _logMessage)
        {
            Description= _logMessage;

        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Description { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public TaskAttachment? Attachment { get; set; }

        public Assignment Assignment { get; set; } = null!;


    }
}
