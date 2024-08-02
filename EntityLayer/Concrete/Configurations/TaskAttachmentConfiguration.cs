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
    sealed internal class TaskAttachmentConfiguration : IEntityTypeConfiguration<TaskAttachment>
    {
        public void Configure(EntityTypeBuilder<TaskAttachment> builder)
        {

            builder.HasKey(a => a.Id);

            builder.HasOne(ta => ta.Assignment)
                .WithMany(a => a.Attachments);

        }
    }
}
