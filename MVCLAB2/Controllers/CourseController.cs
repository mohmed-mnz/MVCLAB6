using Microsoft.AspNetCore.Mvc;
using MVCLAB2.Models;
using System.Net;
using MVCLAB2.viewmodel;
using Microsoft.CodeAnalysis.Differencing;
using MVCLAB2.Repository;
namespace MVCLAB2.Controllers
{
    public class CourseController : Controller
    {
        DB_ITI_Context db;
        ICourseRepository courseRepo;
        IDepartmentRepository departmentRepo;
        public CourseController(ICourseRepository crsRepo, IDepartmentRepository deptRepo)
        {
            this.courseRepo = crsRepo;
            this.departmentRepo = deptRepo;
            db = new DB_ITI_Context();
        }
        public IActionResult Index()
        {
            var Crs=db.Courses.ToList();
            return View(Crs);
        }
        public IActionResult Details(int id)
        {
            Course? course = db.Courses.SingleOrDefault(c => c.Id == id);
            if (course == null)
            {
                // Handle the case when the course is not found
                return NotFound();
            }

            Department? dept = db.Departments.SingleOrDefault(y => y.Id == course.DepartmentId);
            if (dept == null)
            {
                // Handle the case when the department associated with the course is not found
                return NotFound();
            }

            crsDept crd = new crsDept();
            crd.Crsid = (int)course.Id;
            crd.Crsname = course.Name;
            crd.Crsdergree = course.Degree;
            crd.CrsMindergree = course.MinDegree;
            crd.CrsDept = dept.Name;
            return View(crd);
        }
        [HttpGet]
        public IActionResult New() {
            List<Course>crs=db.Courses.ToList();
           List<Department>departments = db.Departments.ToList();
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Course course)
        {
            if (ModelState.IsValid == true)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { 
                List<Department>departments=db.Departments.ToList();
                ViewBag.departments=departments;
                return View(course);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = db.Courses.SingleOrDefault(c => c.Id == id);
            //List<Department>departments=db.Departments.ToList();
            //ViewBag.Departments = departments;
            ViewData["DeptList"] = db.Departments.ToList();
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid == true)
            {
                Course crs = db.Courses.SingleOrDefault(c => c.Id == course.Id);
                crs.Name = course.Name;
                crs.Degree = course.Degree;
                crs.MinDegree = course.MinDegree;
                crs.DepartmentId = course.DepartmentId;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                ViewData["DeptList"] = db.Departments.ToList();
                return View(course);
            }
        }
        public IActionResult Delete(int Id)
        {
            Course course= db.Courses.SingleOrDefault(c => c.Id == Id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult MinDegLesDeg(int minDegree, int degree)
        {
            if (minDegree >= degree)
            {
                return Json("The minimum degree must be less than the degree.");
            }

            return Json(true);
        }

    }
}
