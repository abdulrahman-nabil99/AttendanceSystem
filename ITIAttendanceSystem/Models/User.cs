using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAttendanceSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="* Required")]
        [StringLength(50,MinimumLength =3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [EmailAddress(ErrorMessage ="* Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.Password)]
        [StringLength(32,MinimumLength = 8,ErrorMessage ="Password must be between 8 and 32 characters")]
        public string Password { get; set; }
        public bool? Verified { get; set; }

        [StringLength(120, MinimumLength = 4, ErrorMessage = "Invalid image")]
        public string? UserImg {  get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual UserRole? Role { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        [InverseProperty("Students")]
        public virtual Department? Department { get; set; }

        public int? IntakeId { get; set; }
        public virtual Intake? Intake { get; set; }

        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
        [InverseProperty("Manager")]
        public virtual Department? ManagedDepartment { get; set; }
    }
}
