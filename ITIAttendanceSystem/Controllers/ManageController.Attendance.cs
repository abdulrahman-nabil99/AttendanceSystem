using ITIAttendanceSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Attend(int? id)
        {
            if (DateTime.Now.Hour > 12 || DateTime.Now.Hour < 6)
                return RedirectToAction("index", "Account");
            if (id != null)
            {
                var student = _adminService.UserService.GetById(id.Value);
                if (student != null && student.RoleId ==4)
                {
                    var time = DateTime.Now;
                    AttendanceRecord record = new AttendanceRecord() {
                    UserId = id.Value,
                    AttendanceDate = time,
                    IsPresent = true,
                    };
                    _adminService.AttendanceRecordService.Add(record);
                }
            }
            return RedirectToAction("index", "Account");
        }
        [HttpGet]
        [Route("Manage/Attendance")]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult Attendance(DateTime? targetDate)
        {
            DateTime date = targetDate != null ? targetDate.Value.Date :  DateTime.Now.Date;
            var records = _adminService.AttendanceRecordService.GetAll().Where(ar => ar.AttendanceDate.Date == date);
            ViewBag.Date = date;
            return View(records);
        }
        [Route("Manage/Attendance/Close")]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult CloseAttendance()
        {
            var date = DateTime.Today;
            var students = _adminService.UserService.GetVerifiedStudents();
            foreach (var student in students)
            {
                var todayRecord = student.AttendanceRecords.FirstOrDefault(ar=> ar.AttendanceDate.Date== date);
                if (todayRecord == null)
                    _adminService.AttendanceRecordService.Add(new()
                    {
                        UserId = student.UserId,
                        AttendanceDate = date,
                        IsPresent = false,
                    });
            }

            return RedirectToAction("Attendance", "Manage");
        }
    }
}
