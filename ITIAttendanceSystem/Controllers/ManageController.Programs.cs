using ITIAttendanceSystem.Models;
using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        [Authorize(Roles = "Admin,HR")]
        public IActionResult Programs()
        {
            var programs = _adminService.ProgramService.GetAll();
            return View(programs);
        }

        [Route("Manage/Programs/Add")]
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult AddProgram()
        {
            return View();
        }

        [Route("Manage/Programs/Add")]
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult AddProgram(Models.Program program)
        {
            if (ModelState.IsValid)
            {
                _adminService.ProgramService.Add(program);
            }
            return RedirectToAction("Programs","Manage");
        }

        [Route("Manage/Programs/update/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UpdateProgram(int? id)
        {
            if (id == null) return RedirectToAction("Programs", "Manage");
            var program = _adminService.ProgramService.GetById(id.Value);
            return View("AddProgram",program);
        }

        [Route("Manage/Programs/update/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult UpdateProgram(Models.Program program)
        {
            if (ModelState.IsValid)
            {
                _adminService.ProgramService.Update(program);
            }
            return RedirectToAction("Programs", "Manage");
        }

        [Route("Manage/Programs/delete/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProgram(int? id)
        {
            if (id == null) return RedirectToAction("Programs", "Manage");
            var program = _adminService.ProgramService.GetById(id.Value);
            if (program == null) return RedirectToAction("Programs", "Manage");
            ViewBag.Name = program.ProgramName;
            ViewBag.Id = program.ProgramId;
            ViewBag.FieldName = "ProgramId";
            ViewBag.ActionName = "Programs";
            return View("Delete");
        }

        [Route("Manage/Programs/delete/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProgram()
        {
            if (int.TryParse(Request.Form["ProgramId"], out int id))
            {
                var program = _adminService.ProgramService.GetById(id);
                if (program != null)
                    _adminService.ProgramService.Delete(program);
            }
            return RedirectToAction("Programs", "Manage");

        }
    }
}
