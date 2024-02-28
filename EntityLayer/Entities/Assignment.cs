﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Assignment
    {
        public Assignment()
        {
            UserAssignments = new();
            Progress = ProgressStatus.Published;
            Publisher = new();

            SubTasks = new List<SubTask>();
        }

        [Key]
        public int AssignmentId { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(10000)]
        public string Content { get; set; } = null!;
        public bool Status { get; set; }
        public ProgressStatus Progress { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Due { get; set; }
        public List<UserAssignment> UserAssignments { get; set; } = null!;
        public AppUser Publisher { get; set; } = null!;


        public enum ProgressStatus
        {
            Published,
            InProgress,
            Done,
            Cancelled,
        }

        public List<SubTask> SubTasks { get; set; } = null!;
        public static List<UserAssignment> operator +(Assignment Task,List<int> userList)
        {
            foreach(var userId in  userList)
                Task.UserAssignments.Add(new UserAssignment()
                {
                    AssignmentId = Task.AssignmentId,
                    Assignment = Task,
                    UserId = userId

                });

            return Task.UserAssignments;
        }

    }

}
