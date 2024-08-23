using System.ComponentModel.DataAnnotations;

namespace ITIAttendanceSystem.Models
{
    public class AttendanceRecord
    {
        [Key]
        public int AttendanceRecordId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        public bool IsPresent { get; set; }

    }
}
