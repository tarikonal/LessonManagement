﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Family : BaseEntity
    {

        public string Name { get; set; }
        public ICollection<Student> Members { get; set; }
    }
}

