﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class SubTask
    {
        public SubTask()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public SubTasksProgress Progress { get; set; }

        public Assignment Assignment { get; set; }

      


    }
    public enum SubTasksProgress
    {
        Pending,
        InProgress,
        Done,
    }
}