using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITIAttendanceSystem.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(50,MinimumLength =2)]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Range(1, 500, ErrorMessage = "Capacity must be between 1 and 500")]
        public int DeptCapacity { get; set; }
        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public bool? IsAvailable { get; set; }
        public virtual ICollection<User> Students { get; set; } = new List<User>();
        public virtual User? Manager { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<IntakeDepartments> IntakeDepartments { get; set; } = new List<IntakeDepartments>();

    }
}
