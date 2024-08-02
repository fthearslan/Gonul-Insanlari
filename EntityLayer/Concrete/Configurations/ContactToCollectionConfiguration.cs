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
    sealed internal class ContactToCollectionConfiguration : IEntityTypeConfiguration<ContactToCollection>
    {
        public void Configure(EntityTypeBuilder<ContactToCollection> builder)
        {

            builder.HasKey(to=>to.Id);

            builder.HasOne(to => to.Contact)
                .WithMany(contact => contact.Tos);


        }
    }
}
