using ITIAttendanceSystem.Models;
using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Courses()
        {
            var courses = _adminService.CourseService.GetAll();
            return View(courses);
        }

        [Route("Manage/Courses/Add")]
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult AddCourse()
        {
            return View();
        }

        [Route("Manage/Courses/Add")]
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _adminService.CourseService.Add(course);
            }
            return RedirectToAction("Courses", "Manage");
        }

        [Route("Manage/Courses/update/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult UpdateCourse(int? id)
        {
            if (id == null) return RedirectToAction("Courses", "Manage");
            var course = _adminService.CourseService.GetById(id.Value);
            return View("AddCourse", course);
        }

        [Route("Manage/Courses/update/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult UpdateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _adminService.CourseService.Update(course);
            }
            return RedirectToAction("Courses", "Manage");
        }

        [Route("Manage/Courses/delete/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult DeleteCourse(int? id)
        {
            if (id == null) return RedirectToAction("Courses", "Manage");
            var course = _adminService.CourseService.GetById(id.Value);
            if (course == null) return RedirectToAction("Courses", "Manage");
            ViewBag.Name = course.CourseName;
            ViewBag.Id = course.CourseId;
            ViewBag.FieldName = "CourseId";
            ViewBag.ActionName = "Courses";
            return View("Delete");
        }

        [Route("Manage/Courses/delete/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult DeleteCourse()
        {
            if (int.TryParse(Request.Form["CourseId"], out int id))
            {
                var course = _adminService.CourseService.GetById(id);
                if (course != null)
                    _adminService.CourseService.Delete(course);
            }
            return RedirectToAction("Courses", "Manage");

        }
    }
}
