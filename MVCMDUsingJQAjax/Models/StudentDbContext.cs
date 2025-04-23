using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCMDUsingJQAjax.Models
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext():base("StudentDbContext")
        {
            
        }
        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<CourseModule> Modules { get; set; }

        public virtual DbSet<Student> Students { get; set; }
    }
}