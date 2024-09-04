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
    sealed internal class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {

            builder
                .HasKey(key => new { key.UserId, key.NotificationId });

            builder.HasOne(un => un.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(un => un.UserId);

            builder.HasOne(un => un.Notification)
                .WithMany(n => n.Users)
                .HasForeignKey(un => un.NotificationId);

            builder
                .Navigation(x => x.User)
                .AutoInclude();

            builder
                .Navigation(x => x.Notification)
                .AutoInclude();


        }
    }
}
