using Microsoft.AspNetCore.Mvc;

namespace CourseCenterv2.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService=courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses= await _courseService.GetAllAsync();
            return View(courses);
            
        }

        public async Task<IActionResult> Details (int id)
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
        public async Task<IActionResult>Create(string title,int capacity)
        {
            await _courseService.CreateAsync(title,capacity);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var course =await _courseService.GetByIdAsync(id);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

       public async Task<IActionResult> Edit(int id,String title,int capacity)
        {
            await _courseService.ChangeCapacityAsync(id,capacity);
            await _courseService.ChangeTitleAsync(id,title);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
             return RedirectToAction(nameof(Index));

        }





    }
}