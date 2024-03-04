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
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {

            builder.Navigation(a => a.Publisher)
                .AutoInclude();
            builder.Navigation(a=>a.UserAssignments) 
                .AutoInclude();
            builder.Navigation(a => a.SubTasks)
                .AutoInclude();
        }
    }
}
