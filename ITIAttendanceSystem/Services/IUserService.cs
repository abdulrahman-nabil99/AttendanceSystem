using ITIAttendanceSystem.Models;

namespace ITIAttendanceSystem.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task UpdateUserImage(IFormFile img,User user);
        void RemoveUserImage(User user);
        Task ImportStudentsFromExcelAsync(IFormFile file);
        ICollection<User> GetVerifiedStudents();
        ICollection<User> GetNotVerifiedStudents();
        ICollection<User> GetAllStudents();
        ICollection<User> GetAllHr();
        ICollection<User> GetAllInstructors();
        User GetUserByEmail(string email);
    }
}
