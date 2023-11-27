using EntityLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context:IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-H7OKEVB\\SQLEXPRESS;database=GoDb;integrated security=true; TrustServerCertificate=true;");
        }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Assignment>()
        //        .HasOne(x => x.Sender)
        //        .WithMany(y => y.AssignmentsSent)
        //        .HasForeignKey(z => z.)
        //        .OnDelete(DeleteBehavior.ClientSetNull);

        //    modelBuilder.Entity<Assignment>()
        //        .HasOne(x => x.Receiver)
        //        .WithMany(y => y.ToReceive)
        //        .HasForeignKey(z => z.ReceiverID)
        //        .OnDelete(DeleteBehavior.ClientSetNull);
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<About> Abouts { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Assignment> Assignments { get; set; }
     

    }
}
