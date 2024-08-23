using ITIAttendanceSystem.Models;
using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var users = _adminService.UserService.GetAll().OrderBy(u => u.Role.Id);
            return View(users);
        }

        [Route("Manage/Users/Add")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser()
        {
            var Roles = _adminService.UserRoleService.GetAll();
            ViewBag.Roles = new SelectList(Roles, "Id", "RoleName");
            return View();
        }

        [Route("Manage/Users/Add")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(User user, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                _adminService.UserService.Add(user);
                if (img != null)
                    await _adminService.UserService.UpdateUserImage(img, user);
            }
            else
            {
                ModelState.AddModelError("", "An Error has occured please try again");
                return RedirectToAction("AddUser", "Manage");
            }
            return RedirectToAction("Users","Manage");
        }

        [Route("Manage/Users/update/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(int? id)
        {
            if (id == null) return RedirectToAction("users", "Manage");
            var user = _adminService.UserService.GetById(id.Value);
            if (user == null) return RedirectToAction("users", "Manage");
            var Roles = _adminService.UserRoleService.GetAll();
            ViewBag.Roles = new SelectList(Roles, "Id", "RoleName");
            return View("AddUser",user);
        }

        [Route("Manage/Users/update/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(User user, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                _adminService.UserService.Update(user);
                if (img != null)
                    await _adminService.UserService.UpdateUserImage(img, user);
            }
            else
            {
                ModelState.AddModelError("", "An Error has occured please try again");
                return RedirectToAction("Users", "Manage");
            }
            return RedirectToAction("Users","Manage");
        }

        [Route("Manage/Users/delete/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int? id)
        {
            if (id == null) return RedirectToAction("users", "Manage");
            var user = _adminService.UserService.GetById(id.Value);
            if (user == null) return RedirectToAction("users", "Manage");
            ViewBag.Name = user.Name;
            ViewBag.Id = user.UserId;
            ViewBag.FieldName = "UserId";
            ViewBag.ActionName = "Users";
            return View("Delete");
        }

        [Route("Manage/Users/delete/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser()
        {
            if (int.TryParse(Request.Form["UserId"], out int id))
            {
                var user = _adminService.UserService.GetById(id);
                if (user != null)
                    _adminService.UserService.Delete(user);
            }
            return RedirectToAction("Users", "Manage");

        }
    }
}
