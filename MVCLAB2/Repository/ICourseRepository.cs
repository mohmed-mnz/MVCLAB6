using MVCLAB2.Models;

namespace MVCLAB2.Repository
{
    public class ICourseRepository
    {
        List<Course> GetAll();
        Course GetById(int Id);

        void Add(Course course);

        void Update(Course course, int Id);

        void Delete(int Id);

        void Save();

        Course CheckUnique(string Name, int? Dept_Id);
    }
}
