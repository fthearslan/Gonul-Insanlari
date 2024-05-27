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
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {

            builder.HasKey(ul => ul.Id);
            
            builder.HasOne(ul => ul.User)
                .WithMany(au => au.UserLogin);

        }
    }
}
