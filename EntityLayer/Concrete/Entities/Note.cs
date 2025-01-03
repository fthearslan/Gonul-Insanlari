﻿using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class Note:BaseEntity
    {
        [StringLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
        public AppUser CreatedBy { get; set; }
    }
}
