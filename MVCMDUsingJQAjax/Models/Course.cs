﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMDUsingJQAjax.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public virtual ICollection<Student> Students { get; set; } 
    }
}