using MVCMDUsingJQAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCMDUsingJQAjax.Models.ViewModels;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Reflection;

namespace MVCMDUsingJQAjax.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _db = new StudentDbContext();
        public ActionResult Index()
        {
            var applicants = _db.Students
            .Include(s => s.Modules)
            .Include(s => s.Course).ToList();
            return View(applicants);
        }
      
        public ActionResult Create()
        {
            StudentViewModel student = new StudentViewModel();
             student.Courses = _db.Courses.ToList();
            student.Modules.Add(new CourseModule() { CourseModuleId = 1 });

            return View(student);
        }
        [HttpPost]

        public ActionResult Create(StudentViewModel student)
        {
            string uniqueFileName = GetImageName(student.ProfileFile);
            student.ImageUrl = uniqueFileName;
            Student obj = new Student();
            obj.StudentName = student.StudentName;
            obj.CourseId = student.CourseId;
            obj.MobileNo = student.MobileNo;
            obj.IsEnrolled = student.IsEnrolled;
            obj.Dob = student.Dob;
            obj.ImageUrl = student.ImageUrl;
            _db.Students.Add(obj);
            _db.SaveChanges();
            var user = _db.Students.FirstOrDefault(x => x.MobileNo == student.MobileNo);
            if (user != null)
            {
                if (student.Modules.Count > 0)
                {
                    foreach (var item in student.Modules)
                    {
                        CourseModule objM = new CourseModule();
                        objM.StudentId = user.StudentId;
                        objM.Duration = item.Duration;
                        objM.ModuleName = item.ModuleName;
                        _db.Modules.Add(objM);
                    }
                }
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            var app = _db.Students.Find(id);
            var existsModules = _db.Modules.Where(e => e.StudentId == id).ToList();
            foreach (var exp in existsModules)
            {
                _db.Modules.Remove(exp);
            }
            _db.Entry(app).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string GetImageName(HttpPostedFileBase file)
        {
            string filePath = "";
            if (file != null)
            {
                filePath = Path.Combine("/Images/", Guid.NewGuid().ToString()) + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath(filePath));
            }
            return filePath;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var student = _db.Students.Include(c => c.Course).Include(c => c.Modules).FirstOrDefault(c => c.StudentId == id);
            if (student == null) { return HttpNotFound("Student not found"); }
            var viewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                CourseId = student.CourseId,
                CourseName = student.Course.CourseName,
                MobileNo = student.MobileNo,
                IsEnrolled = student.IsEnrolled,
                ImageUrl = student.ImageUrl,
                Courses = _db.Courses.ToList(),
                Modules = student.Modules.ToList()
            };
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName", student.CourseId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel student)
        {
            Student existingStudent = _db.Students
                    .Include(a => a.Modules)
                    .FirstOrDefault(x => x.StudentId == student.StudentId);

            if (existingStudent != null)
            {
                existingStudent.StudentName = student.StudentName;
                existingStudent.CourseId = student.CourseId;
                existingStudent.MobileNo = student.MobileNo;
                existingStudent.IsEnrolled = student.IsEnrolled;
                existingStudent.Dob = student.Dob;

                if (student.ProfileFile != null)
                {
                    string uniqueFileName = GetImageName(student.ProfileFile);
                    existingStudent.ImageUrl = uniqueFileName;
                }

                
                var existingModules = _db.Modules.Where(m => m.StudentId == existingStudent.StudentId).ToList();
                foreach (var mod in existingModules)
                {
                    _db.Modules.Remove(mod);
                }

                
                if (student.Modules != null)
                {
                    foreach (var item in student.Modules)
                    {
                        if (!string.IsNullOrWhiteSpace(item.ModuleName))
                        {
                            var newModule = new CourseModule
                            {
                                StudentId = existingStudent.StudentId,
                                ModuleName = item.ModuleName,
                                Duration = item.Duration
                            };
                            _db.Modules.Add(newModule);
                        }
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        
    }
}