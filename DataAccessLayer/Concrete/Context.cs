using EntityLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-H7OKEVB\\SQLEXPRESS;database=GoDb;integrated security=true; TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Article>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AppUserID);
        }


        public DbSet<About> Abouts { get; set; } = null!;
        public DbSet<Announcement> Announcements { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<NewsLetter> NewsLetters { get; set; } = null!;

        public DbSet<Assignment> Assignments { get; set; } = null!;

        public DbSet<Message> Messages { get; set; } = null!;

        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;



    }
}
