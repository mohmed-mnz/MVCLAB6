using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLAB2.Models
{
    public class CrsResult
    {
        public int? Id { get; set; }
        public int Degree { get; set; }
        [ForeignKey(nameof(Course))]
        public int? CourseId { get; set; }
        [ForeignKey(nameof(Trainee))]
        public int? TraineeId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual Trainee? Trainee { get; set; }
    }
}
