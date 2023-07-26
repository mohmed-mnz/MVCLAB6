using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLAB2.Models
{
    public class Course
    {
        [Key]
        public int? Id { get; set; }
        [UniqueCourseName]
        public string? Name { get; set; }
        [Range(50,100)]
        public int Degree { get; set; }
        [Remote("MinDegLesDeg","Course",ErrorMessage ="MinDigree Must < Degree",AdditionalFields = "Degree")]
        public int MinDegree { get; set; }
        public virtual List<CrsResult>? CourseResults { get; set; }
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual List<Instructor>? Instructors { get; set; }
    }
}
