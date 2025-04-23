using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMDUsingJQAjax.Models
{
    public class CourseModule
    {
        public int CourseModuleId { get; set; }

        public string ModuleName { get; set; } 

        public int Duration { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; } 
    }
}