using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLAB2.Models
{
    public class Instructor
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? image { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }
        [ForeignKey(nameof(Course))]
        public int? CourseId { get; set; }
        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }

        public virtual Course? course { get; set; }
        public virtual Department? department { get; set; }

    }
}
