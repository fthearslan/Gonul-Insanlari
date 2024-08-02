using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class ContactToCollection
    {
        public ContactToCollection()
        {
            
        }
        public ContactToCollection(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string EmailAddress { get; set; }

        public Contact Contact { get; set; }


    }
}
