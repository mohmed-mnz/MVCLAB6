using MVCLAB2.Models;

namespace MVCLAB2.Repository
{
    public class CourseRepository : ICourseRepository
    {
        DB_ITI_Context context;
        public CourseRepository(DB_ITI_Context context)
        {
            this.context = context;
        }
        public void Add(Course course)
        {
            context.Courses.Add(course);
        }
        public void Delete(int Id)
        {
            Course course = context.Courses.SingleOrDefault(c => c.Id == Id);
            context.Courses.Remove(course);
        }
        public List<Course> GetAll()
        {
            return context.Courses.ToList();
        }

        public Course GetById(int Id)
        {
            return context.Courses.SingleOrDefault(c => c.Id == Id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Course course, int Id)
        {
            Course oldCrs = context.Courses.SingleOrDefault(c => c.Id == Id);

            oldCrs.Name = course.Name;
            oldCrs.Degree = course.Degree;
            oldCrs.MinDegree = course.MinDegree;
            oldCrs.DepartmentId = course.DepartmentId;
        }

        public Course CheckUnique(string Name, int? Dept_Id)
        {
            return context.Courses.SingleOrDefault(c => c.Name == Name && c.DepartmentId == Dept_Id);
        }
    }
}
