using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAttendanceSystem.Models
{
    public class UserRole
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } 
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
