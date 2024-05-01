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
    public class TaskLogConfiguration : IEntityTypeConfiguration<TaskLog>
    {
        public void Configure(EntityTypeBuilder<TaskLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Assignment)
                .WithMany(x => x.Logs);
              

        }
    }
}
