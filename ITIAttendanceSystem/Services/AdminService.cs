using ITIAttendanceSystem.Models;

namespace ITIAttendanceSystem.Services
{
    public class AdminService : IAdminService
    {
        public IUserService UserService { get; }
        public IGenericService<UserRole> UserRoleService { get; }
        public IGenericService<AttendanceRecord> AttendanceRecordService { get; }
        public IGenericService<Course> CourseService { get; }
        public IGenericService<Department> DepartmentService { get; }
        public IGenericService<Intake> IntakeService { get; }
        public IGenericService<IntakeDepartments> IntakeDepartmentsService { get; }
        public IGenericService<Models.Program> ProgramService { get; }
        public IGenericService<UserCourses> UserCoursesService { get; }

        public AdminService(
            IUserService userService,
            IGenericService<UserRole> userRoleService,
            IGenericService<AttendanceRecord> attendanceRecordService,
            IGenericService<Course> courseService,
            IGenericService<Department> departmentService,
            IGenericService<Intake> intakeService,
            IGenericService<IntakeDepartments> intakeDepartmentsService,
            IGenericService<Models.Program> programService,
            IGenericService<UserCourses> userCoursesService
            )
        {
            UserService = userService;
            UserRoleService = userRoleService;
            AttendanceRecordService = attendanceRecordService;
            CourseService = courseService;
            DepartmentService = departmentService;
            IntakeService = intakeService;
            IntakeDepartmentsService = intakeDepartmentsService;
            ProgramService = programService;
            UserCoursesService = userCoursesService;
        }
    }
}
