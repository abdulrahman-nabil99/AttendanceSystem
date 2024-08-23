using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITIAttendanceSystem.Models
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, MinimumLength = 2)]
        public string CourseName { get; set; }
        public int? CourseDuration { get; set; }
        public virtual ICollection<UserCourses> UserCourses { get; set; } = new List<UserCourses>();
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    }
}
