using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITIAttendanceSystem.Models
{
    public class UserCourses
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [Range(0, 100)]
        public int? Grade { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }
}
