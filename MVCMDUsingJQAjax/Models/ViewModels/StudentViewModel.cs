using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMDUsingJQAjax.Models.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]        
     
        public DateTime Dob { get; set; } = DateTime.Now;

        public string MobileNo { get; set; }

        public string ImageUrl { get; set; }

        public bool IsEnrolled { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public int Duration { get; set; }


        public virtual Course Course { get; set; }
        public List<Course> Courses { get; set; }


        public virtual IList<CourseModule> Modules { get; set; } = new List<CourseModule>();

        public IList<Student> Students { get; set; }
        public HttpPostedFileBase ProfileFile { get; set; }
    }
}