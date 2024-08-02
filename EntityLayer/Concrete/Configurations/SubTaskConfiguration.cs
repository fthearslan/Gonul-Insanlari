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
    sealed internal class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
    {
        public void Configure(EntityTypeBuilder<SubTask> builder)
        {
            builder.HasKey(s=>s.Id);

            builder.HasOne(st => st.Assignment)
                .WithMany(a => a.SubTasks);


        }
    }
}
