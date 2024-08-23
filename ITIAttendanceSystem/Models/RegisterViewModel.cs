using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAttendanceSystem.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "* Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.Password)]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 32 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "* Required")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Remote("CheckIntakeDepartments","Account",AdditionalFields = "DepartmentId", ErrorMessage ="Unavailable Department")]
        public int IntakeId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? UserImg {  get; set; }
    }
}
