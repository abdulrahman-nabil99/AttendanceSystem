using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace ITIAttendanceSystem.Models
{
    public class AttendanceDbContext:DbContext
    {
        public AttendanceDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Intake> Intakes { get; set; }
        public DbSet<IntakeDepartments> IntakeDepartments { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCourses>().HasKey((uc) => new { uc.UserId, uc.CourseId });
            modelBuilder.Entity<IntakeDepartments>().HasKey((id) => new { id.IntakeId,id.DepartmentId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User {UserId=1, Name="Admin",Email="admin@admin.com",Password="Pass1234",RoleId=1, Verified=true});
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { RoleName = "Admin", Id=1 },
                new UserRole { RoleName = "HR", Id = 2 },
                new UserRole { RoleName = "Instructor", Id = 3 },
                new UserRole { RoleName = "Student", Id = 4 }
                );
        }



    }
}
