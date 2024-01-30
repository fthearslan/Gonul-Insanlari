﻿using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Configurations
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {

            builder.Property(x => x.Created)
                .HasDefaultValue(DateTime.Now);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.UserId);

        }
    }
}