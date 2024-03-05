using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class NewsLetter:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MailAddress { get; set; }

    }
}
