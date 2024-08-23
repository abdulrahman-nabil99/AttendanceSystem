using ITIAttendanceSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        [Authorize(Roles = "Admin,HR")]
        public IActionResult Intakes()
        {
            var intakes = _adminService.IntakeService.GetAll();
            return View(intakes);
        }

        [Route("Manage/Intakes/Add")]
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult AddIntake()
        {
            ViewBag.Programs = new SelectList(_adminService.ProgramService.GetAll(), "ProgramId", "ProgramName");
            return View();
        }

        [Route("Manage/Intakes/Add")]
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult AddIntake(Intake intake)
        {
            if (ModelState.IsValid)
            {
                _adminService.IntakeService.Add(intake);
            }
            return RedirectToAction("Intakes", "Manage");
        }


        [Route("Manage/Intakes/update/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UpdateIntake(int? id)
        {
            if (id == null) return RedirectToAction("Intakes", "Manage");
            var intake = _adminService.IntakeService.GetById(id.Value);
            if ( intake.IsOpen == false)
                return RedirectToAction("Intakes", "Manage");
            ViewBag.Programs = new SelectList(_adminService.ProgramService.GetAll(), "ProgramId", "ProgramName");
            return View("AddIntake", intake);
        }

        [Route("Manage/Intakes/update/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UpdateIntake(Intake intake)
        {
            if (ModelState.IsValid)
            {
                _adminService.IntakeService.Update(intake);
            }
            return RedirectToAction("Intakes", "Manage");
        }


        [Route("Manage/Intakes/delete/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteIntake(int? id)
        {
            if (id == null) return RedirectToAction("Intakes", "Manage");
            var intake = _adminService.IntakeService.GetById(id.Value);
            if (intake == null) return RedirectToAction("Intakes", "Manage");
            ViewBag.Name = intake.IntakeName;
            ViewBag.Id = intake.IntakeId;
            ViewBag.FieldName = "IntakeId";
            ViewBag.ActionName = "Intakes";
            return View("Delete");
        }

        [Route("Manage/Intakes/delete/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteIntake()
        {
            if (int.TryParse(Request.Form["IntakeId"], out int id))
            {
                var intake = _adminService.IntakeService.GetById(id);
                if (intake != null)
                    _adminService.IntakeService.Delete(intake);
            }
            return RedirectToAction("Intakes", "Manage");

        }

        [Route("Manage/Intakes/updatedepartments/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UpdateIntakeDepartments(int? id)
        {
            if (id == null) return RedirectToAction("Intakes", "Manage");
            var intake = _adminService.IntakeService.GetById(id.Value);
            if (intake== null || intake.IsOpen == false)
                return RedirectToAction("Intakes", "Manage");
            ViewBag.Departments = _adminService.DepartmentService.GetAll().Where(d => d.IsAvailable == true);
            return View(intake);
        }

        [Route("Manage/Intakes/updatedepartments/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UpdateIntakeDepartments(int[] departments)
        {
            if (int.TryParse(Request.Form["IntakeId"], out int id))
            {
                var intake = _adminService.IntakeService.GetById(id);
                if (intake == null || (departments.Length <= 0 && intake.IntakeDepartments.Count <= 0))
                    return RedirectToAction("Intakes", "Manage");
                intake.IntakeDepartments.Clear();
                foreach (var departmentId in departments)
                {
                    Department d = _adminService.DepartmentService.GetById(departmentId);
                    if (d == null) continue;
                    IntakeDepartments intakeDepartments = new()
                    {
                        DepartmentId = departmentId,
                        IntakeId = id,
                        NumberOfStudents = d.DeptCapacity,
                    };
                    intake.IntakeDepartments.Add(intakeDepartments);
                }
                _adminService.CourseService.SaveChanges();

            }
            return RedirectToAction("Intakes", "Manage");
        }
    }
}
