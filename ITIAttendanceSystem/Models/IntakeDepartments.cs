using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAttendanceSystem.Models
{
    public class IntakeDepartments
    {
        [ForeignKey("Intake")]
        public int IntakeId { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public int? NumberOfStudents { get; set; }
        public virtual Intake Intake { get; set; }
        public virtual Department Department { get; set; }
    }
}
