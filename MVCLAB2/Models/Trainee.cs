using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLAB2.Models
{
    public class Trainee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Grade { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; } 
        public Department? Department { get; set; }
        public virtual List<CrsResult>? CresResults { get; set; }
    }

}
