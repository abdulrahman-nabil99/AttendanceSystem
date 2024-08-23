using ITIAttendanceSystem.Models;
using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {

        bool CheckDepartmentInstructor(Department department)
        {
            if (!User.IsInRole("Admin"))
            {
                if (department.ManagerId == null || !int.TryParse(User.FindFirst("id")?.Value, out int instId) && department.ManagerId != instId)
                    return false;
            }
            return true;
        }
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Departments()
        {
            var departments = _adminService.DepartmentService.GetAll();
            return View(departments);
        }

        [Route("Manage/Departments/Add")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDepartment()
        {
            var instructors = _adminService.UserService.GetAllInstructors();
            ViewBag.Instructors = new SelectList(instructors, $"UserId", "Name");
            return View();
        }

        [Route("Manage/Departments/Add")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _adminService.DepartmentService.Add(department);
            }
            return RedirectToAction("Departments", "Manage");
        }

        [Route("Manage/Departments/update/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult UpdateDepartment(int? id)
        {
            if (id == null) return RedirectToAction("Departments", "Manage");
            var department = _adminService.DepartmentService.GetById(id.Value);
            if (!CheckDepartmentInstructor(department)) return RedirectToAction("Departments", "Manage");
            var instructors = _adminService.UserService.GetAllInstructors();
            ViewBag.Instructors = new SelectList(instructors, $"UserId", "Name");
            return View("AddDepartment", department);
        }

        [Route("Manage/Departments/update/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult UpdateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                if (!CheckDepartmentInstructor(department)) return RedirectToAction("Departments", "Manage");

                _adminService.DepartmentService.Update(department);
            }
            return RedirectToAction("Departments", "Manage");
        }

        [Route("Manage/Departments/delete/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDepartment(int? id)
        {
            if (id == null) return RedirectToAction("Departments", "Manage");
            var department = _adminService.DepartmentService.GetById(id.Value);
            if (department == null) return RedirectToAction("Departments", "Manage");
            ViewBag.Name = department.DeptName;
            ViewBag.Id = department.DeptId;
            ViewBag.FieldName = "DeptId";
            ViewBag.ActionName = "Departments";
            return View("Delete");
        }

        [Route("Manage/Departments/delete/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDepartment()
        {
            if (int.TryParse(Request.Form["DeptId"], out int id))
            {
                var department = _adminService.DepartmentService.GetById(id);
                if (department != null)
                    _adminService.DepartmentService.Delete(department);
            }
            return RedirectToAction("Departments", "Manage");

        }

        [Route("Manage/Departments/UpdateCourses/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult UpdateDepartmentCourses(int? id)
        {
            if (id == null) return RedirectToAction("Departments", "Manage");
            var department = _adminService.DepartmentService.GetById(id.Value);
            if (department == null) return RedirectToAction("Departments", "Manage");
            if (!CheckDepartmentInstructor(department)) return RedirectToAction("Departments", "Manage");

            ViewBag.Courses = _adminService.CourseService.GetAll();
            return View(department);
        }

        [Route("Manage/Departments/UpdateCourses/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult UpdateDepartmentCourses(int[] courses)
        {
            if (int.TryParse(Request.Form["DeptId"], out int id))
            {
                var department = _adminService.DepartmentService.GetById(id);
                if (department == null || (courses.Length<=0 && department.Courses.Count<=0))
                    return RedirectToAction("Departments", "Manage");
                if (!CheckDepartmentInstructor(department)) return RedirectToAction("Departments", "Manage");

                department.Courses.Clear();
                foreach (var courseId in courses) 
                {
                    Course c = _adminService.CourseService.GetById(courseId);
                    if (c == null) continue;
                    department.Courses.Add(c);
                }
                _adminService.CourseService.SaveChanges();

            }
            return RedirectToAction("Departments", "Manage");
        }
    }
}
