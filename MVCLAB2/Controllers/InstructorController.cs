using Microsoft.AspNetCore.Mvc;
using MVCLAB2.Models;
using MVCLAB2.viewmodel;

namespace MVCLAB2.Controllers
{
    public class InstructorController : Controller
    {
        DB_ITI_Context db;
        public InstructorController()
        {
            db = new DB_ITI_Context();
        }
        public IActionResult Index()
        {
            var ins=db.Instructors.ToList();
            return View(ins);
        }

        public IActionResult Detail(int id)
        {
            Instructor? ins = db.Instructors.SingleOrDefault(x => x.Id == id);
            if (ins == null)
            {
                return NotFound();
            }

            Department? dept = db.Departments.SingleOrDefault(y => y.Id == ins.DeptId);
            if (dept == null)
            {
                return NotFound();
            }

            Course? crs = db.Courses.SingleOrDefault(c => c.Id == ins.CourseId);
            if (crs == null)
            {
                return NotFound();
            }

            InsDeptCrs MD = new InsDeptCrs();
            MD.Ins_Id = (int)ins.Id;
            MD.Ins_Name = ins.Name;
            MD.Ins_Image = ins.image;
            MD.Ins_Address = ins.Address;
            MD.Ins_Salary = ins.Salary;
            MD.Ins_Department = dept.Name;
            MD.Ins_Course = crs.Name;

            return View(MD);
        }
        [HttpGet]
        public IActionResult New()
        {
            List<Department> departments = db.Departments?.ToList() ?? new List<Department>();
            ViewBag.Depts = departments;
            ViewData["DeptList"] = departments;

            List<Course> courses = db.Courses?.ToList() ?? new List<Course>();
            ViewBag.Courses = courses;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> New(Instructor instructor)
        {
            if (instructor != null)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(instructor);
            }

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Instructor instructor = db.Instructors.SingleOrDefault(x => x.Id == Id);
            List<Department> departments = db.Departments?.ToList() ?? new List<Department>();
            ViewBag.Depts = departments;
            ViewData["DeptList"] = departments;

            List<Course> courses = db.Courses?.ToList() ?? new List<Course>();
            ViewBag.Courses = courses;
            return View(instructor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Instructor instructor)
        {

            Instructor inst = db.Instructors.SingleOrDefault(x => x.Id == instructor.Id);
            inst.Name = instructor.Name;
            inst.Salary = instructor.Salary;
            inst.Address = instructor.Address;
            inst.DeptId = instructor.DeptId;
            inst.CourseId = instructor.CourseId;


            inst.image = instructor.image;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string Name)
        {
            if (Name != "")
            {
                var insts = db.Instructors.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
                return View("Index", insts);
            }
            else
            {
                var instructors = db.Instructors.ToList();

                return View("Index", instructors);
            }

        }

        public IActionResult Delete(int Id)
        {
            Instructor instructor = db.Instructors.SingleOrDefault(x => x.Id == Id);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult GetCourseByDept(int dept_id)
        {
            List<Course> courses = db.Courses.Where(n => n.DepartmentId == dept_id).ToList();
            return Json(courses);
        }

    }
}
