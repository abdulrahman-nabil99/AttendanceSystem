using ITIAttendanceSystem.Models;
using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;

namespace ITIAttendanceSystem.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        IGenericService<UserRole> _userRoleService;
        IGenericService<Department> _departmentService;
        IGenericService<Intake> _intakeService;
        IGenericService<Program> _programService;
        public AccountController(
            IUserService userService,
            IGenericService<UserRole> userRoleService,
            IGenericService<Department> departmentService,
            IGenericService<Intake> intakeService,
            IGenericService<Program> programService
            )
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _departmentService = departmentService;
            _intakeService = intakeService;
            _programService = programService;
        }
        [Authorize]
        public IActionResult Index()
        {
            // to be implemented with MyAccount
            if(int.TryParse(User.FindFirst("id")?.Value,out int id))
            {
                var user = _userService.GetById(id);
                return View(user);
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult AccessDenied()
        { 
            return View();
        }  
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginData)
        {
            if (ModelState.IsValid)
            {
                var account = _userService.GetUserByEmail(loginData.Email);
                if (account != null && account.Verified == true)
                {
                    ClaimsIdentity cp = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    cp.AddClaims([
                        new (ClaimTypes.Name, account.Name),
                        new (ClaimTypes.Role, account.Role.RoleName??"Student"),
                        new ("Email", account.Email),
                        new ("id",account.UserId.ToString()),
                        ]);

                    var user = new ClaimsPrincipal(cp);
                    await HttpContext.SignInAsync(user);
                    return RedirectToAction("index", "home");
                } else
                {
                    ModelState.AddModelError("", "You Will be able to login when you get Verified");
                    return View();
                }
            }
            ModelState.AddModelError("", "Invalid Email or Password");
            return View();
        }
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            var depts = _departmentService.GetAll().Where(d => d.IsAvailable == true);
            var intakes = _intakeService.GetAll().Where(d => d.IsOpen == true);
            ViewBag.Departments = new SelectList(depts, "DeptId", "DeptName");
            ViewBag.Intakes = new SelectList(intakes, "IntakeId", "IntakeName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    RoleId = 4,
                    Name = registerModel.Name,
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    DepartmentId = registerModel.DepartmentId,
                    IntakeId = registerModel.IntakeId,
                };
                _userService.Add(user);
                if (registerModel.UserImg != null)
                     await _userService.UpdateUserImage(registerModel.UserImg, user);
            }
            else
            {
                ModelState.AddModelError("", "An Error has occured please try again");
                return RedirectToAction("Register","Account");
            }
            return Redirect("/account/login");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }
        public IActionResult CheckIntakeDepartments(int DepartmentId, int? IntakeId)
        {
            var department = _departmentService.GetById(DepartmentId);
            if (department == null || !department.IsAvailable==true || IntakeId == null)
            {
                return Json(false);
            }

            var intake = _intakeService.GetById(IntakeId.Value);
            if (intake == null)
            {
                return Json(false); 
            }

            var intakeDept = intake.IntakeDepartments.FirstOrDefault(id => id.DepartmentId == DepartmentId);
            return Json(intakeDept != null); 
        }

        [Authorize]
        public async Task<IActionResult> UpdateUserImage(IFormFile? img)
        {
            if (!int.TryParse(User.FindFirst("id")?.Value, out int userId))
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("index", "Home");
            }
            if (img != null )
            {
                var user = _userService.GetById(userId);
                if (user != null)
                    await _userService.UpdateUserImage(img, user);
                else
                {
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("index", "Home");
                }
            }
            return RedirectToAction("index", "Account");
        }
    }
}
