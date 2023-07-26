using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCLAB2.Models
{
    public class UniqueCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string name = value.ToString();
            
            if (string.IsNullOrEmpty(name) || name.Length < 2 || name.Length > 20)
            {
                return new ValidationResult("Course name must be between 2 and 20 characters long.");
            }
            
            Course coursefromreq = validationContext.ObjectInstance as Course;
            
            DB_ITI_Context context = new DB_ITI_Context();
            
            Course course = context.Courses.FirstOrDefault(c => c.Name == name && c.DepartmentId == coursefromreq.DepartmentId);
            
            if (course == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Course name must be unique within the department.");
            }
        }
    }
}
