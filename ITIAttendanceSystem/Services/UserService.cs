using ITIAttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Data;

namespace ITIAttendanceSystem.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(AttendanceDbContext _context) : base(_context)
        {
        }

        public ICollection<User> GetAllHr()
        {
            return _dbSet.Where(user=> user.RoleId==2).ToList();
        }

        public ICollection<User> GetAllInstructors()
        {
            return _dbSet.Where(user => user.RoleId == 3).ToList();
        }

        public ICollection<User> GetAllStudents()
        {
            return _dbSet.Where(user => user.RoleId==4).ToList();
        }

        public ICollection<User> GetNotVerifiedStudents()
        {
            return _dbSet.Where(user => user.RoleId == 4 && user.Verified != true).ToList();
        }

        public User GetUserByEmail(string email)
        {
            var user = _dbSet.SingleOrDefault(user => user.Email == email);
            return user;    
        }

        public ICollection<User> GetVerifiedStudents()
        {
            return _dbSet.Where(user => user.RoleId == 4 && user.Verified == true).ToList();
        }

        public void RemoveUserImage(User user)
        {
            if (!user.UserImg.IsNullOrEmpty())
            {
                var filePath = Path.Combine("wwwroot/imgs/users/", user.UserImg);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        public async Task UpdateUserImage(IFormFile img, User user)
        {
            string[] ext = { "jpg", "png", "jpeg", "gif" };
            if (user == null || img == null) return ;
            RemoveUserImage(user);
            var extension = img.FileName.Split('.').Last().ToLower();
            if (!ext.Contains(extension)) return;
            var fileName = $"user-{user.UserId}-pfimg.{extension}";
            using (FileStream file = new($"wwwroot/imgs/users/{fileName}", FileMode.Create))
            {
                await img.CopyToAsync(file);
            }
            user.UserImg = fileName;
            Update(user);
        }
        public async Task ImportStudentsFromExcelAsync(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var name = worksheet.Cells[row, 2].Text;
                        var email = worksheet.Cells[row, 3].Text;
                        var departmentId = worksheet.Cells[row, 4].Text;
                        var intakeId = worksheet.Cells[row, 5].Text;

                        var user = new User
                        {
                            Name = name,
                            Email = email,
                            Password = "100200Art",
                            DepartmentId = int.Parse(departmentId),
                            IntakeId = int.Parse(intakeId),
                            RoleId = 4
                        };

                        Add(user);
                    }
                }
            }
        }
    }
}
