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
    public class UserAssignmentConfiguration : IEntityTypeConfiguration<UserAssignment>
    {
        public void Configure(EntityTypeBuilder<UserAssignment> builder)
        {
            builder.HasKey(k => new { k.UserId, k.AssignmentId });

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAssignments)
                .HasForeignKey(ua => ua.UserId);

            builder.HasOne(ua => ua.Assignment)
                .WithMany(a => a.UserAssignments)
                .HasForeignKey(ua => ua.AssignmentId);

        }
    }
}
