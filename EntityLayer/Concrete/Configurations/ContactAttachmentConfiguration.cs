using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Configurations
{
    sealed internal class ContactAttachmentConfiguration : IEntityTypeConfiguration<ContactAttachment>
    {
        public void Configure(EntityTypeBuilder<ContactAttachment> builder)
        {

            builder.HasKey(attachment => attachment.Id);
            builder.HasOne(attachment => attachment.Contact)
               .WithMany(contact => contact.Attachments);

        }
    }
}
