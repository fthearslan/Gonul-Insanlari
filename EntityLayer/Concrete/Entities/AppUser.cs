using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            UserLogin = new();
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; }=null!;
        
        public int Age { get; set; } 
        public string ImagePath { get; set; } = null!;

        public string SocialMediaAccount { get; set; }
        public string AboutMe { get; set; } = null!;
        public bool Status { get; set; } 

        public DateTime Registered { get; set; } = DateTime.Now;
        public List<Article> Articles { get; set; } = null!;

        public List<Announcement> Announcements { get; set; } = null!;

        public List<UserAssignment>? UserAssignments { get; set; } = null!;

        [InverseProperty("Sender")]
        public List<Message> MessagesSent { get; set; } = null!;
        [InverseProperty("Receiver")]
        public List<Message> MessagesReceived { get; set; } = null!;
        public List<Note> Notes { get; set; } = null!;

        public List<UserLogin> UserLogin { get; set; }

        [TempData]
        [NotMapped]
        public IList<string> Roles { get; set; } = null!;

    }
}
