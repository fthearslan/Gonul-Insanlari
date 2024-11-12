using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Providers
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=surface\\DEVMSSQLSERVER;database=GoDb;integrated security=true; TrustServerCertificate=true;", options =>
            {
                options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);

            });
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.Load(nameof(EntityLayer)));

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
        public DbSet<UserAssignment> UserAssignment { get; set; } = null!;

        public DbSet<SubTask> SubTask { get; set; } = null!;

        public DbSet<TaskAttachment> TaskAttachment { get; set; } = null!;
        public DbSet<TaskLog> TaskLogs { get; set; } = null!;

        public DbSet<UserLogin> UsersLogins { get; set; } = null!;

        public DbSet<ContactAttachment> ContactAttachments { get; set; } = null!;

        public DbSet<UserNotification> UserNotifications { get; set; } = null!;

        public DbSet<Visitor> Visitors { get; set; } = null!;
    }


}
