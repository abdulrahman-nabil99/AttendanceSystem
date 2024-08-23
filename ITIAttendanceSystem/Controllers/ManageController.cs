using ITIAttendanceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        IAdminService _adminService;
        public ManageController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin,HR,Instructor")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
