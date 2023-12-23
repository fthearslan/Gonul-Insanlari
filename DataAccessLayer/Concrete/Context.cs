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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ArticleVideo>()
                           .HasKey(av => new { av.ArticleID, av.VideoID });

            builder.Entity<ArticleVideo>()
                .HasOne(av => av.Article)
                .WithMany(a => a.Videos)
                .HasForeignKey(a => a.ArticleID);

            builder.Entity<ArticleVideo>()
                .HasOne(av => av.Video)
                .WithMany(v => v.Articles)
                .HasForeignKey(v => v.VideoID);


        }





        public DbSet<About> Abouts { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ArticleVideo> ArticleVideos { get; set; }


    }
}
