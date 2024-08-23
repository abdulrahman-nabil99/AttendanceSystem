using System.ComponentModel.DataAnnotations;

namespace ITIAttendanceSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Length(8, 55)]
        public string Password { get; set; }
    }
}
