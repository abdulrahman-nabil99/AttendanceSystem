using iText.Kernel.Pdf;
using iText.Layout.Element;
using ITIAttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using iText.Layout;
using Microsoft.AspNetCore.Authorization;

namespace ITIAttendanceSystem.Controllers
{
    public partial class ManageController : Controller
    {
        [Authorize(Roles = "Admin,HR")]
        public IActionResult Students(string? filter)
        {
            IEnumerable<User> students;
            if (filter?.ToLower() == "verified")
                students = _adminService.UserService.GetVerifiedStudents();
            else if (filter?.ToLower() == "unverified")
                students = _adminService.UserService.GetNotVerifiedStudents();
            else
                students = _adminService.UserService.GetAllStudents();
            return View(students);
        }
        [Route("Manage/Students/Verify/{id?}")]
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult VerifyStudent(int? id)
        {
            if (id == null) return RedirectToAction("Students", "Manage");
            var student = _adminService.UserService.GetById(id.Value);
            if (student == null || student.RoleId !=4)
                return RedirectToAction("Students", "Manage");
            student.Verified = true;
            _adminService.UserService.SaveChanges();
            return RedirectToAction("Students", "Manage");
        }

        [HttpGet]
        [Route("Manage/Students/AddBulk")]
        [Authorize(Roles = "Admin,HR")]
        public IActionResult ImportStudents()
        {
            return View();
        }

        [HttpPost]
        [Route("Manage/Students/AddBulk")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> ImportStudents(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                await _adminService.UserService.ImportStudentsFromExcelAsync(file);
                return RedirectToAction("Students", "Manage"); 
            }

            ModelState.AddModelError("", "Please upload a valid Excel file.");
            return View();
        }

        [Route("/Mycard/{id:int}")]
        [HttpGet]
        [Authorize]
        public IActionResult PrintStudentCard(int id)
        {
            return GenerateStudentCard(id);
        }

        [Authorize]
        public IActionResult GenerateStudentCard(int studentId)
        {
            var student = _adminService.UserService.GetById(studentId);

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var imagePath = $"wwwroot/imgs/users/{(student.UserImg is null ? "defaulimg.jpg":student.UserImg)}";
                var image = new Image(iText.IO.Image.ImageDataFactory.Create(imagePath));

                document.Add(image.SetWidth(150).SetHeight(150));
                document.Add(new Paragraph(student.Name).SetBold().SetFontSize(20));
                document.Add(new Paragraph($"Program: {student?.Intake?.Program?.ProgramName ?? ""}"));
                document.Add(new Paragraph($"Intake: {student?.Intake?.IntakeName ?? ""}"));
                document.Add(new Paragraph($"Department: {student?.Department?.DeptName ?? ""}"));

                document.Close();

                var fileName = $"{student?.Name ?? ""}-StudentCard.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}
