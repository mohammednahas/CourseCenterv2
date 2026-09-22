using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseCenterv2.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);

        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, int capacity)
        {
            await _courseService.CreateAsync(title, capacity);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(
    int id,
    string title,
    int capacity,
    uint version)
        {
            try
            {
                await _courseService.UpdateAsync(
                    id,
                    title,
                    capacity,
                    version);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                var course = await _courseService.GetByIdAsync(id);

                if (course is null)
                {
                    return NotFound();
                }

                ModelState.AddModelError(
                    string.Empty,
                    "This course was modified by another user. Your changes were not saved.");

                return View(course);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> AddPrefix()
        {
            await _courseService.AddPrefix();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProgrammingCourses()
        {
            await _courseService.DeleteProgrammingcourses();
            return RedirectToAction(nameof(Index));

        }





    }
}