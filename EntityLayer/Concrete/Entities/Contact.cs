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

        public Contact()
        {

            Attachments = new();
            Tos = new();
       

        }

        public Contact(ContactStatus status)
        {
             ContactStatus = status;

            Attachments = new();
            Tos = new();

        }

        public string? NameSurname { get; set; }


        [StringLength(50)]
        public string From { get; set; }

        public string Content { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        public bool IsSeen { get; set; }

        public string? SenderId { get; set; }

        public ContactStatus ContactStatus { get; set; } = ContactStatus.Pending;


        public List<ContactToCollection> Tos { get; set; }

        public List<ContactAttachment> Attachments { get; set; }

        public Contact? RepliedTo { get; set; }


    }

    public enum ContactStatus
    {

        Pending,
        Received,
        Sent,
        Drafted,
        Newsletter,
        Trash

    }

  



}
