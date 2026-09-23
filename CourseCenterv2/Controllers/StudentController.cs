using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Application.ViewModels;
using CourseCenterv2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenterv2.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public StudentController(
            IStudentService studentService,
            ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();
            return View(students);

            // return Content("wwwwww");
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student is null)
                return NotFound();

            var courses = await _courseService.GetAllAsync();

            var viewModel = new StudentDetailsViewModel
            {
                Student = student,
                Courses = courses.ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string email)
        {
            await _studentService.CreateAsync(name, email);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            string name,
            string email)
        {
            await _studentService.RenameAsync(id, name, email);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(
            int studentid,
            int courseid)
        {
            await _studentService.EnrollInCourseAsync(
                studentid,
                courseid);

            return RedirectToAction(
                nameof(Details),
                new { id = studentid });
        }
    }
}