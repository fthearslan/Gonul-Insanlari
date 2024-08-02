using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class ContactAttachment:BaseAttachment
    {
        public ContactAttachment()
        {
            Contact = new();
        }

        public ContactAttachment(Contact contact)
        {

            Contact = contact;
        }

        public ContactAttachment(string path)
        {
            Path = path;
        }

        public ContactAttachment(Contact contact, string path)
        {
            Contact = contact;
            Path = path;
        }

      
        public Contact Contact { get; set; }
       
    }
}
