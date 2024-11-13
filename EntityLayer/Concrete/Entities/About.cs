﻿using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    public class About : BaseEntity
    {

        [StringLength(75)]
        public string Title { get; set; }
        public string Details { get; set; }



    }
}
