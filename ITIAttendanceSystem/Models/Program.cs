using System.ComponentModel.DataAnnotations;

namespace ITIAttendanceSystem.Models
{
    public class Program
    {
        [Key]
        public int ProgramId { get; set; }

        [Required( ErrorMessage = "* Required")]
        [StringLength(100,MinimumLength = 2)]
        public string ProgramName { get; set; }
        public int? ProgramDuration { get; set; }
        public bool? IsActive { get; set; }
        public virtual ICollection<Intake> Intakes { get; set; } = new List<Intake>();
    }
}
