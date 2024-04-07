﻿using Microsoft.AspNetCore.Identity;
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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }

        public List<Article> Articles { get; set; }

        public List<Announcement> Announcements { get; set; }

        public List<UserAssignment>? UserAssignments { get; set; }

        [InverseProperty("Sender")]
        public List<Message> MessagesSent { get; set; }
        [InverseProperty("Receiver")]
        public List<Message> MessagesReceived { get; set; }
        public List<Note> Notes { get; set; }


    }
}