using ITIAttendanceSystem.Models;

namespace ITIAttendanceSystem.Services
{
    public interface IAdminService
    {
        IUserService UserService { get; }
        IGenericService<UserRole> UserRoleService {  get; }
        IGenericService<AttendanceRecord> AttendanceRecordService {  get; }
        IGenericService<Course> CourseService {  get; }
        IGenericService<Department> DepartmentService {  get; }
        IGenericService<Intake> IntakeService {  get; }
        IGenericService<IntakeDepartments> IntakeDepartmentsService {  get; }
        IGenericService<Models.Program> ProgramService {  get; }
        IGenericService<UserCourses> UserCoursesService {  get; }
    }
}
