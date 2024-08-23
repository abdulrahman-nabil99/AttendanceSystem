using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAttendanceSystem.Models
{
    public class Intake
    {
        [Key]
        public int IntakeId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(100, MinimumLength = 2)]
        public string IntakeName { get; set; }
        public bool? IsOpen { get; set; }
        [ForeignKey("Program")]
        [Required(ErrorMessage = "* Required")]
        public int ProgramId { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<IntakeDepartments> IntakeDepartments { get; set; } = new List<IntakeDepartments>();
        public virtual Program? Program { get; set; }
    }
}
